--[[
    The PauseScreenState is the Pause screen of the game, shown when P is Pressed. It should
    display "Press Enter"
]]

PauseScreenState = Class{__includes = BaseState}

function PauseScreenState:update(dt)
    -- transition to countdown when enter/return are pressed
    if love.keyboard.wasPressed('enter') or love.keyboard.wasPressed('return') then
        sounds['music']:play()
        gStateMachine:change('countdown')
    end
end

function PauseScreenState:render()
    -- simple UI code
    sounds['music']:pause()
    love.graphics.setFont(flappyFont)
    love.graphics.printf('Pause', 0, 64, VIRTUAL_WIDTH, 'center')

    love.graphics.setFont(mediumFont)
    love.graphics.printf('Press Enter to Return to Game', 0, 100, VIRTUAL_WIDTH, 'center')
end