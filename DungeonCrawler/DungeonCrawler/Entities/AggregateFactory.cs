﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DungeonCrawler.Components;

namespace DungeonCrawler.Entities
{
    /// <summary>
    /// A class for quickly creating entities from pre-defined collections
    /// of components
    /// </summary>
    public class AggregateFactory
    {
        /// <summary>
        /// The game this AggregateFactory belongs to
        /// </summary>
        DungeonCrawlerGame game;

        /// <summary>
        /// Creates a new AggregateFactory instance
        /// </summary>
        /// <param name="game"></param>
        public AggregateFactory(DungeonCrawlerGame game)
        {
            this.game = game;
        }

        /// <summary>
        /// Creates Entities from aggregates (collections of components)
        /// </summary>
        /// <param name="aggregate">The specific aggreage to create</param>
        public uint CreateFromAggregate(Aggregate aggregate, PlayerIndex playerIndex)
        {
            uint entityID = 0xFFFFFF;
            Texture2D spriteSheet;
            Position position;
            Movement movement;
            MovementSprite movementSprite;
            Local local;
            Player player;
            PlayerInfo info;
            Stats stats = new Stats();
            HUDAggregateFactory hudagg = new HUDAggregateFactory(game);
            InvAggregateFactory invagg = new InvAggregateFactory(game);

            //Miscelaneous modifyers for the potential ability modifiers
            //Placeholders for racial/class bonuses and item bonuses.
            int miscMeleeAttack=0;
            int miscRangedAttack = 0;
            int miscMeleeSpeed=0;
            int miscAccuracy=0;
            int miscMeleeDef=0;
            int miscRangedDef=0;
            int miscSpell=0;
            int miscHealth=0;

            switch (aggregate)
            {
                case Aggregate.FairyPlayer:
                    entityID = Entity.NextEntity();
                    spriteSheet = game.Content.Load<Texture2D>("Spritesheets/wind_fae");
                    spriteSheet.Name = "Spritesheets/wind_fae";

                    position = new Position()
                    {
                        EntityID = entityID,
                        Center = new Vector2(400, 50),
                        Radius = 32f,
                    };
                    game.PositionComponent[entityID] = position;

                    movement = new Movement()
                    {
                        EntityID = entityID,
                        Direction = new Vector2(0, 1),
                        Speed = 200f,
                    };
                    game.MovementComponent[entityID] = movement;

                    movementSprite = new MovementSprite()
                    {
                        EntityID = entityID,
                        Facing = Facing.South,
                        SpriteSheet = spriteSheet,
                        SpriteBounds = new Rectangle(0, 0, 64, 64),
                        Timer = 0f,
                    };
                    game.MovementSpriteComponent[entityID] = movementSprite;

                    local = new Local()
                    {
                        EntityID = entityID,
                    };
                    game.LocalComponent[entityID] = local;

                    //This will add a stats section for the player in the stats component
                    stats = new Stats()
                    {
                        EntityID = entityID,

                        //So here we just define our base values. Total sum is 50
                        //The base stats are 10 across the board
                        Strength = 4,
                        Stamina = 10,
                        Agility = 10,
                        Intelligence = 16,
                        Defense = 10
                    };
                    game.StatsComponent[entityID] = stats;

                    player = new Player()
                    {
                        EntityID = entityID,
                        PlayerIndex = playerIndex,
                        abilityModifiers = new AbilityModifiers()
                        {
                            meleeDamageReduction = miscMeleeDef + (int)((stats.Defense-10)/2),
                            rangedDamageReduction = miscRangedDef + (int)((stats.Defense - 10) / 2),
                            meleeAttackBonus = miscMeleeAttack + (int)((stats.Strength - 10) / 2),
                            RangedAttackBonus = miscRangedAttack + (int)((stats.Agility - 10) / 2),
                            MeleeAttackSpeed = miscMeleeSpeed + (int)((stats.Strength - 10) / 2),
                            Accuracy = miscAccuracy + (int)((stats.Agility - 10) / 2),
                            SpellBonus = miscSpell + (int)((stats.Intelligence - 10) / 2),
                            HealthBonus = miscHealth + (int)((stats.Stamina - 10) / 2),
                        }
                    };
                    game.PlayerComponent[entityID] = player;

                    info = new PlayerInfo()
                    {
                        Health = 100,
                        Psi = 100,
                        State = PlayerState.Default,
                    };

                    break;

                case Aggregate.CultistPlayer:
                    entityID = Entity.NextEntity();
                    spriteSheet = game.Content.Load<Texture2D>("Spritesheets/Cultist");
                    spriteSheet.Name = "Spritesheets/Cultist";

                    position = new Position()
                    {
                        EntityID = entityID,
                        Center = new Vector2(400, 50),
                        Radius = 32f,
                    };
                    game.PositionComponent[entityID] = position;
                    
                    movement = new Movement() {
                        EntityID = entityID,
                        Direction = new Vector2(0, 1),
                        Speed = 200f,
                    };
                    game.MovementComponent[entityID] = movement;
                    
                    movementSprite = new MovementSprite() {
                        EntityID = entityID,
                        Facing = Facing.South,
                        SpriteSheet = spriteSheet,
                        SpriteBounds = new Rectangle(0, 0, 64, 64),
                        Timer = 0f,
                    };
                    game.MovementSpriteComponent[entityID] = movementSprite;
                    
                    local = new Local(){
                        EntityID = entityID,
                    };
                    game.LocalComponent[entityID] = local;

                    //This will add a stats section for the player in the stats component
                    stats = new Stats()
                    {
                        EntityID = entityID,

                        //So here we just define our base values. Total sum is 50
                        //The base stats are 10 across the board
                        Strength = 4,
                        Stamina = 10,
                        Agility = 10,
                        Intelligence = 16,
                        Defense = 10
                    };

                    player = new Player()
                    {
                        EntityID = entityID,
                        PlayerIndex = PlayerIndex.One,
                        abilityModifiers = new AbilityModifiers()
                        {
                            meleeDamageReduction = miscMeleeDef + (int)((stats.Defense - 10) / 2),
                            rangedDamageReduction = miscRangedDef + (int)((stats.Defense - 10) / 2),
                            meleeAttackBonus = miscMeleeAttack + (int)((stats.Strength - 10) / 2),
                            RangedAttackBonus = miscRangedAttack + (int)((stats.Agility - 10) / 2),
                            MeleeAttackSpeed = miscMeleeSpeed + (int)((stats.Strength - 10) / 2),
                            Accuracy = miscAccuracy + (int)((stats.Agility - 10) / 2),
                            SpellBonus = miscSpell + (int)((stats.Intelligence - 10) / 2),
                            HealthBonus = miscHealth + (int)((stats.Stamina - 10) / 2),
                        }

                    };

                    info = new PlayerInfo()
                    {
                        Health = 100,
                        Psi = 100,
                        State = PlayerState.Default,
                    };

                    game.PlayerComponent[entityID] = player;
                    //Create HUD
                    hudagg.CreateHUD(player);
                    //create Inv
                    invagg.CreateInv(player);

                    break;

                case Aggregate.CyborgPlayer:
                    entityID = Entity.NextEntity();
                    spriteSheet = game.Content.Load<Texture2D>("Spritesheets/cyborg");
                    spriteSheet.Name = "Spritesheets/cyborg";

                    position = new Position()
                    {
                        EntityID = entityID,
                        Center = new Vector2(400, 50),
                        Radius = 32f,
                    };
                    game.PositionComponent[entityID] = position;

                    movement = new Movement()
                    {
                        EntityID = entityID,
                        Direction = new Vector2(0, 1),
                        Speed = 200f,
                    };
                    game.MovementComponent[entityID] = movement;

                    movementSprite = new MovementSprite()
                    {
                        EntityID = entityID,
                        Facing = Facing.South,
                        SpriteSheet = spriteSheet,
                        SpriteBounds = new Rectangle(0, 0, 64, 64),
                        Timer = 0f,
                    };
                    game.MovementSpriteComponent[entityID] = movementSprite;

                    local = new Local()
                    {
                        EntityID = entityID,
                    };
                    game.LocalComponent[entityID] = local;

                    //This will add a stats section for the player in the stats component
                    stats = new Stats()
                    {
                        EntityID = entityID,

                        //So here we just define our base values. Total sum is 50
                        //The base stats are 10 across the board
                        Strength = 10,
                        Stamina = 10,
                        Agility = 10,
                        Intelligence = 10,
                        Defense = 10
                    };
                    game.StatsComponent[entityID] = stats;

                    player = new Player()
                    {
                        EntityID = entityID,
                        PlayerIndex = PlayerIndex.One,

                    };

                    info = new PlayerInfo()
                    {
                        Health = 100,
                        Psi = 100,
                        State = PlayerState.Default,
                    };

                    game.PlayerComponent[entityID] = player;
                    //create HUD
                    hudagg.CreateHUD(player);
                    //create Inv
                    invagg.CreateInv(player);
                    break;

                case Aggregate.EarthianPlayer:
                    entityID = Entity.NextEntity();
                    spriteSheet = game.Content.Load<Texture2D>("Spritesheets/Earthian2x");
                    spriteSheet.Name = "Spritesheets/Earthian2x";
                    
                    position = new Position()
                    {
                        EntityID = entityID,
                        Center = new Vector2(400, 50),
                        Radius = 32f,
                    };
                    game.PositionComponent[entityID] = position;
                    
                    movement = new Movement() {
                        EntityID = entityID,
                        Direction = new Vector2(0, 1),
                        Speed = 200f,
                    };
                    game.MovementComponent[entityID] = movement;
                    
                    movementSprite = new MovementSprite() {
                        EntityID = entityID,
                        Facing = Facing.South,
                        SpriteSheet = spriteSheet,
                        SpriteBounds = new Rectangle(0, 0, 64, 64),
                        Timer = 0f,
                    };
                    game.MovementSpriteComponent[entityID] = movementSprite;
                    
                    local = new Local(){
                        EntityID = entityID,
                    };
                    game.LocalComponent[entityID] = local;

                    //This will add a stats section for the player in the stats component
                    stats = new Stats()
                    {
                        EntityID = entityID,

                        //So here we just define our base values. Total sum is 50
                        //The base stats are 10 across the board
                        Strength = 10,
                        Stamina = 10,
                        Agility = 10,
                        Intelligence = 10,
                        Defense = 10
                    };
                    game.StatsComponent[entityID] = stats;

                    player = new Player()
                    {
                        EntityID = entityID,
                        PlayerIndex = playerIndex,
                    };
                    game.PlayerComponent[entityID] = player;

                    info = new PlayerInfo()
                    {
                        Health = 100,
                        Psi = 100,
                        State = PlayerState.Default,
                    };
                    game.PlayerInfoComponent[entityID] = info;
                    
                    //Create HUD
                    hudagg.CreateHUD(player);
                    //create Inv
                    invagg.CreateInv(player);
                    break;

                case Aggregate.GargranianPlayer:
                    entityID = Entity.NextEntity();
                    spriteSheet = game.Content.Load<Texture2D>("Spritesheets/gargranian");
                    spriteSheet.Name = "Spritesheets/gargranian";

                    position = new Position()
                    {
                        EntityID = entityID,
                        Center = new Vector2(400, 50),
                        Radius = 32f,
                    };
                    game.PositionComponent[entityID] = position;

                    movement = new Movement()
                    {
                        EntityID = entityID,
                        Direction = new Vector2(0, 1),
                        Speed = 200f,
                    };
                    game.MovementComponent[entityID] = movement;

                    movementSprite = new MovementSprite()
                    {
                        EntityID = entityID,
                        Facing = Facing.South,
                        SpriteSheet = spriteSheet,
                        SpriteBounds = new Rectangle(0, 0, 64, 64),
                        Timer = 0f,
                    };
                    game.MovementSpriteComponent[entityID] = movementSprite;

                    local = new Local()
                    {
                        EntityID = entityID,
                    };
                    game.LocalComponent[entityID] = local;

                    //This will add a stats section for the player in the stats component
                    stats = new Stats()
                    {
                        EntityID = entityID,

                        //So here we just define our base values. Total sum is 50
                        //The base stats are 10 across the board
                        Strength = 10,
                        Stamina = 10,
                        Agility = 10,
                        Intelligence = 10,
                        Defense = 10
                    };
                    game.StatsComponent[entityID] = stats;

                    player = new Player()
                    {
                        EntityID = entityID,
                        PlayerIndex = playerIndex,
                    };
                    game.PlayerComponent[entityID] = player;

                    info = new PlayerInfo()
                    {
                        Health = 100,
                        Psi = 100,
                        State = PlayerState.Default,
                    };
                    game.PlayerInfoComponent[entityID] = info;
                    
                    //Create HUD
                    hudagg.CreateHUD(player);
                    //create Inv
                    invagg.CreateInv(player);
                    break;

                case Aggregate.SpacePiratePlayer:
                    entityID = Entity.NextEntity();
                    spriteSheet = game.Content.Load<Texture2D>("Spritesheets/SpacePBig");
                    spriteSheet.Name = "Spritesheets/SpacePBig";

                    position = new Position()
                    {
                        EntityID = entityID,
                        Center = new Vector2(400, 50),
                        Radius = 32f,
                    };
                    game.PositionComponent[entityID] = position;

                    movement = new Movement()
                    {
                        EntityID = entityID,
                        Direction = new Vector2(0, 1),
                        Speed = 200f,
                    };
                    game.MovementComponent[entityID] = movement;

                    movementSprite = new MovementSprite()
                    {
                        EntityID = entityID,
                        Facing = Facing.South,
                        SpriteSheet = spriteSheet,
                        SpriteBounds = new Rectangle(0, 0, 64, 64),
                        Timer = 0f,
                    };
                    game.MovementSpriteComponent[entityID] = movementSprite;

                    local = new Local()
                    {
                        EntityID = entityID,
                    };
                    game.LocalComponent[entityID] = local;

                    //This will add a stats section for the player in the stats component
                    stats = new Stats()
                    {
                        EntityID = entityID,

                        //So here we just define our base values. Total sum is 50
                        //The base stats are 10 across the board
                        Strength = 5,
                        Stamina = 5,
                        Agility = 25,
                        Intelligence = 10,
                        Defense = 5
                    };
                    game.StatsComponent[entityID] = stats;

                    player = new Player()
                    {
                        EntityID = entityID,
                        PlayerIndex = PlayerIndex.One,
                    };
                    game.PlayerComponent[entityID] = player;

                    info = new PlayerInfo()
                    {
                        Health = 100,
                        Psi = 100,
                        State = PlayerState.Default,
                    };
                    game.PlayerInfoComponent[entityID] = info;
                    
                    //Create HUD
                    hudagg.CreateHUD(player);
                    //create Inv
                    invagg.CreateInv(player);
                    break;

                case Aggregate.ZombiePlayer:
                    entityID = Entity.NextEntity();
                    spriteSheet = game.Content.Load<Texture2D>("Spritesheets/MzombieBx2");
                    spriteSheet.Name = "Spritesheets/MzombieBx2";

                    //Placeholder values
                    miscMeleeAttack = 5;
                    miscMeleeDef = 5;
                    miscRangedDef = -5;

                    position = new Position()
                    {
                        EntityID = entityID,
                        Center = new Vector2(400, 50),
                        Radius = 32f,
                    };

                    game.PositionComponent[entityID] = position;

                    movement = new Movement()
                    {
                        EntityID = entityID,
                        Direction = new Vector2(0, 1),
                        Speed = 200f,
                    };
                    game.MovementComponent[entityID] = movement;

                    movementSprite = new MovementSprite()
                    {
                        EntityID = entityID,
                        Facing = Facing.South,
                        SpriteSheet = spriteSheet,
                        SpriteBounds = new Rectangle(0, 0, 64, 64),
                        Timer = 0f,
                    };
                    game.MovementSpriteComponent[entityID] = movementSprite;

                    local = new Local()
                    {
                        EntityID = entityID,
                    };
                    game.LocalComponent[entityID] = local;

                    //This will add a stats section for the player in the stats component
                    stats = new Stats()
                    {
                        EntityID = entityID,

                        //So here we just define our base values. Total sum is 50
                        //The base stats are 10 across the board
                        Strength = 16,
                        Stamina = 5,
                        Agility = 5,
                        Intelligence = 10,
                        Defense = 14
                    };
                    game.StatsComponent[entityID] = stats;

                    player = new Player()
                    {
                        EntityID = entityID,
                        PlayerIndex = PlayerIndex.One,
                        abilityModifiers = new AbilityModifiers()
                        {
                            meleeDamageReduction = miscMeleeDef + (int)((stats.Defense - 10) / 2),
                            rangedDamageReduction = miscRangedDef + (int)((stats.Defense - 10) / 2),
                            meleeAttackBonus = miscMeleeAttack + (int)((stats.Strength - 10) / 2),
                            RangedAttackBonus = miscRangedAttack + (int)((stats.Agility - 10) / 2),
                            MeleeAttackSpeed = miscMeleeSpeed + (int)((stats.Strength - 10) / 2),
                            Accuracy = miscAccuracy + (int)((stats.Agility - 10) / 2),
                            SpellBonus = miscSpell + (int)((stats.Intelligence - 10) / 2),
                            HealthBonus = miscHealth + (int)((stats.Stamina - 10) / 2),
                        }
                    };

                    game.PlayerComponent[entityID] = player;

                    info = new PlayerInfo()
                    {
                        Health = 100,
                        Psi = 100,
                        State = PlayerState.Default,
                    };
                    game.PlayerInfoComponent[entityID] = info;
                    
                    //Create HUD
                    hudagg.CreateHUD(player);
                    //create Inv
                    invagg.CreateInv(player);
                    break;

                default:
                    throw new Exception("Unknown type.");
            }

            return entityID;
        }
    }
}
