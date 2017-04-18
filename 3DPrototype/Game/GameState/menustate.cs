using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace _3DPrototype.Game.GameState
{
    enum Button
    {
        Play,
        Quit,
        Count
    }

    class menustate : AGameState
    {
        //Auswahl und Positionierung von Sprites
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 1400, 780), Color.White);
            spriteBatch.Draw(startbutton, new Vector2(600, 240), selected == Button.Play ? Color.Green : Color.White);
            spriteBatch.Draw(quitbutton, new Vector2(600, 500), selected == Button.Quit ? Color.Green : Color.White);
            
            spriteBatch.End();

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            var newPressedKeys = from k in newState.GetPressedKeys()
                                 where !(oldState_.GetPressedKeys().Contains(k))
                                 select k;
            if (newPressedKeys.Contains(Keys.Down))
            {
                if (selected < Button.Quit)  selected++;
            }
            else if(newPressedKeys.Contains(Keys.Up))
            {
                if(selected > Button.Play) selected--;
            }
            else if(newPressedKeys.Contains(Keys.Enter))
            {
                if (selected == Button.Play) NewState = new MainState(Globals.Camera);
                else if (selected == Button.Quit) IsFinished = true;
            }
            oldState_ = newState;
            
        }
        


        //Constructor, erstellt eine Klasse mit Namen Batch
        //die ein SpriteBatch ist
        public menustate(SpriteBatch Batch)
        { 
            spriteBatch = Batch;
            background = Globals.ContentManager.Load<Texture2D>("Arrow");
            startbutton = Globals.ContentManager.Load<Texture2D>("startbutton");
            quitbutton = Globals.ContentManager.Load<Texture2D>("exitbutton");
        }
        //Parameter erstellen
        SpriteBatch spriteBatch;
        private Texture2D background;
        private Texture2D startbutton;
        private Texture2D quitbutton;
        private int selection_;
        Button selected;
        KeyboardState oldState_;
      
    }
    

}
