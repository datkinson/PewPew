using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using UnityEngine.UI;

public class LuaScriptController : MonoBehaviour
{
    private RadarHUDControler radarHudController;
    private WeaponControl weaponController;
    private InputController inputController;
    private GameObject debugConsole;
    private Text debugConsoleText;
    public int debugConsoleLines = 10;
    private string debugConsoleLinePadding = "   ";
    private List<string> debugConsoleBuffer = new List<string>{};
    public float radarRange = 50;
    public InputField scriptingInputField;
    private CannonController cannonController;

    /*private string luaScript = @"
        number = 2;
        doubledNumber = DoubleInput(number);
        position = GetPlayerPosition();
        enemies = GetEnemiesInRange();
        DebugPrint(enemies[0]['distanceToTarget']);
        -- DebugPrint(position['x']);
        -- DebugPrint(position['y']);
        FireMissile();
    ";*/
    private string luaScript = @"";
    private string defaultScript = @"
enemies = GetObjectsInRange('Enemy');
enemyDistance = 100000000;
enemyX = 0
enemyY = 0
targetCount = 0
for index, value in pairs(enemies) do
    targetCount = targetCount + 1
    if value['distanceToTarget'] < enemyDistance then
        enemyDistance = value['distanceToTarget']
        enemyX = value['x']
        enemyY = value['y']
    end
end
if targetCount > 0 then
    TargetCannon(enemyX, enemyY);
    EnableCannonTrigger(true);
else
    EnableCannonTrigger(false);
end";
    private Script script;
    // Start is called before the first frame update
    void Start()
    {
        radarHudController = GameObject.FindWithTag("Player").GetComponent<RadarHUDControler>();
        weaponController = GameObject.FindWithTag("Player").GetComponent<WeaponControl>();
        inputController = GameObject.FindWithTag("Player").GetComponent<InputController>();
        cannonController = GameObject.FindWithTag("Player").GetComponentInChildren<CannonController>();
        debugConsole = GameObject.FindWithTag("Console");
        debugConsoleText = GameObject.FindWithTag("ConsoleText").GetComponent<Text>();
        debugConsole.SetActive(false);
        script = new Script();
        luaScript = PlayerPrefs.GetString("luaScript");
        scriptingInputField.text = luaScript;
        if (!PlayerPrefs.HasKey("luaScript"))
        {
            ResetToDefaultScript();
        }
        script.Globals["DoubleInput"] = (System.Func<int, int>) DoubleInput;
        script.Globals["DebugPrint"] = (System.Action<string>)DebugPrint;
        script.Globals["Thrust"] = (System.Action<float>)Thrust;
        script.Globals["Brake"] = (System.Action)Brake;
        script.Globals["Rotate"] = (System.Action<float>)Rotate;
        script.Globals["FireMissile"] = (System.Action)FireMissile;
        script.Globals["DeployMine"] = (System.Action)DeployMine;
        script.Globals["GetPlayerPosition"] = (System.Func<Script, Table>)GetPlayerPosition;
        script.Globals["GetPlayerRotation"] = (System.Func<Script, Table>)GetPlayerRotation;
        script.Globals["GetObjectsInRange"] = (System.Func<string, IDictionary>)GetObjectsInRange;
        script.Globals["EnableCannonTrigger"] = (System.Action<bool>)EnableCannonTrigger;
        script.Globals["TargetCannon"] = (System.Action<float, float>)TargetCannon;
        script.Globals["RotateTowards"] = (System.Action<float, float>)RotateTowards;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            script.DoString(luaScript);
        }
        catch (ScriptRuntimeException ex)
        {
            DebugPrint("Runtime Error: " + ex.DecoratedMessage);
        }
        catch (SyntaxErrorException ex)
        {
            DebugPrint("Syntax Error: " + ex.DecoratedMessage);
        }
        catch (InternalErrorException ex)
        {
            DebugPrint("Internal Error: " + ex.DecoratedMessage);
        }
    }

    public void SetLuaScript(string script)
    {
        Debug.Log("LUA script update triggered: " + scriptingInputField.text);
        //Debug.Log("LUA script set to: " + script);
        luaScript = scriptingInputField.text;
        PlayerPrefs.SetString("luaScript", luaScript);
        PlayerPrefs.Save();
    }

    public void ResetToDefaultScript()
    {
        scriptingInputField.text = defaultScript;
        SetLuaScript(defaultScript);
    }

    int DoubleInput(int input)
    {
        int output = input * 2;
        return output;
    }

    void DebugPrint(string line)
    {
        WriteToDebugConsole(line);
    }

    void WriteToDebugConsole(string line)
    {
        debugConsoleBuffer.Add(line);
        if (debugConsoleBuffer.Count > debugConsoleLines)
        {
            debugConsoleBuffer.RemoveAt(0);
        }
        // Render buffer to console
        string consoleDisplay = "";
        foreach(string consoleLine in debugConsoleBuffer)
        {
            /*if (consoleDisplay == "")
            {
                consoleDisplay = consoleLine;
            } else
            {
                consoleDisplay += "\n" + consoleLine;
            }*/
            consoleDisplay += "\n" + debugConsoleLinePadding + consoleLine;
        }
        if (consoleDisplay != "")
        {
            debugConsole.SetActive(true);
        }
        Debug.Log(line);
        debugConsoleText.text = consoleDisplay;
    }

    void FireMissile()
    {
        weaponController.FireMissile();
    }

    void DeployMine()
    {
        weaponController.DeployMine();
    }

    void Thrust(float amount)
    {
        inputController.ThrustForward(amount);
    }

    void Rotate(float amount)
    {
        inputController.Rotate(amount);
    }

    void Brake()
    {
        inputController.Brake();
    }

    Table GetPlayerPosition(Script script)
    {
        Table playerPosition = new Table(script);
        playerPosition["x"] = transform.position.x;
        playerPosition["y"] = transform.position.y;
        //playerPosition["z"] = transform.position.z;
        return playerPosition;
    }

    Table GetPlayerRotation(Script script)
    {
        Table playerRotation = new Table(script);
        playerRotation["x"] = transform.rotation.x;
        playerRotation["y"] = transform.rotation.y;
        playerRotation["z"] = transform.rotation.z;
        return playerRotation;
    }

    IDictionary GetObjectsInRange(string thing)
    {
        IDictionary enemies = new Dictionary<int, IDictionary>();
        GameObject[] targets = GameObject.FindGameObjectsWithTag(thing);
        int itterator = 0;
        foreach(GameObject target in targets)
        {
            Rigidbody2D shipRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
            Rigidbody2D targetRigidbody = target.GetComponent<Rigidbody2D>();
            var xDifference = Mathf.Abs(shipRigidbody.position.x - targetRigidbody.position.x);
            var yDifference = Mathf.Abs(shipRigidbody.position.y - targetRigidbody.position.y);
            if (xDifference < radarRange && yDifference < radarRange)
            {
                IDictionary enemyPosition = new Dictionary<string, float>();
                enemyPosition["x"] = targetRigidbody.position.x;
                enemyPosition["y"] = targetRigidbody.position.y;
                //enemyPosition["z"] = targetRigidbody.position.z;
                enemyPosition["distanceToTarget"] = Vector3.Distance(targetRigidbody.position, shipRigidbody.position);
                enemies[itterator] = enemyPosition;
                itterator++;
            }
        }
        return enemies;
    }

    void TargetCannon(float x, float y)
    {
        Vector2 target = new Vector2(x, y);
        cannonController.SetTarget(target);
    }

    void EnableCannonTrigger(bool state)
    {
        cannonController.EnableTrigger(state);
    }

    void RotateTowards(float x, float y)
    {
        Vector2 target = new Vector2(x, y);
        inputController.RotateTowards(target);
    }
}
