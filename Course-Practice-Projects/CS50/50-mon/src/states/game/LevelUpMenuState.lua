LevelUpMenuState = Class{__includes = BaseState}

function LevelUpMenuState:init(playerPokemon,HPIncrease, attackIncrease, defenseIncrease, speedIncrease)
    self.LevelUpMenu = Menu {
        x = VIRTUAL_WIDTH - 184,
        y = VIRTUAL_HEIGHT - 214,
        width = 184,
        height = 124,
        items = {
            {
                text = 'Level: ' .. tostring(playerPokemon.level) .. ' + 1 = ' .. tostring(playerPokemon.level + 1), 
            },
            {
                text = 'HP: ' .. tostring(playerPokemon.HP) .. ' + '.. tostring(HPIncrease) .. ' = ' .. tostring(playerPokemon.HP + HPIncrease), 
            },
            {
                text = 'Attack: ' .. tostring(playerPokemon.attack) .. ' + '.. tostring(attackIncrease) .. ' = ' .. tostring(playerPokemon.attack + attackIncrease), 
            },
            {
                text = 'Defense: ' .. tostring(playerPokemon.defense) .. ' + '.. tostring(defenseIncrease) .. ' = ' .. tostring(playerPokemon.defense + defenseIncrease), 
            },
            {
                text = 'Speed: ' .. tostring(playerPokemon.speed) .. ' + '.. tostring(speedIncrease) .. ' = ' .. tostring(playerPokemon.speed + speedIncrease), 
            }
        }
    }

    self.LevelUpMenu.selection.cursorEnabled = false
end

function LevelUpMenuState:update(dt)  
    self.LevelUpMenu:update(dt)
end

function LevelUpMenuState:render()
    self.LevelUpMenu:render()
end