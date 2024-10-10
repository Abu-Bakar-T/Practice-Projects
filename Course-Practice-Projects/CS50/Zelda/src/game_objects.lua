--[[
    GD50
    Legend of Zelda

    Author: Colton Ogden
    cogden@cs50.harvard.edu
]]

GAME_OBJECT_DEFS = {
    ['switch'] = {
        type = 'switch',
        texture = 'switches',
        frame = 2,
        width = 16,
        height = 16,
        solid = false,
        defaultState = 'unpressed',
        states = {
            ['unpressed'] = {
                frame = 2
            },
            ['pressed'] = {
                frame = 1
            }
        }
    },
    ['hearts'] = {
        type = 'hearts',
        texture = 'hearts',
        frame = 5,
        width = 16,
        height = 16,
        defaultState = 'unConsumed',
        solid = false,
        consumeable = true,
        consumed = false,
        states = {
            ['unConsumed'] = {
                frame = 5
            },
            ['consumed'] = {
                consumed = true
            }
        }
    },
    ['pot'] = {
        -- TODO
        type = 'pot',
        texture = 'pot',
        frame = math.random(4,6),
        width = 16,
        height = 16,
        opacity = 255,
        bump = false,
        solid = true,
        defaultState = 'unpicked',
        collidable = true,
        pickable = true,
        states = {
            ['unpicked'] = {
                frame = math.random(4,6)
            },
            ['picked'] = {
                frame = math.random(1,3)
            },
            ['broken'] = {
                frame = math.random(7,9)
            }
        ---- empty pot = 1-3
        -- filled pot = 7-9
        -- locked pot = 4-6
    }
    }
}