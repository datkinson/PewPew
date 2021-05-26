enemies = GetObjectsInRange('Enemy');
enemyDistance = 100000000
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
end