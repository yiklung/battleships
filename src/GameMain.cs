using System;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
            //Open the game window
            SwinGame.OpenGraphicsWindow("GameMain", 800, 600);
            SwinGame.ShowSwinGameSplashScreen();
            
			//Shape myShape = new Shape ();


            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                
                //Clear the screen and draw the framerate
                SwinGame.ClearScreen(Color.White);
                SwinGame.DrawFramerate(0,0);
                
				//click for rectangle
				myShape.Draw ();

				if(SwinGame.MouseClicked(MouseButton.LeftButton))
					{
					myShape.x = SwinGame.MouseX();
					myShape.y = SwinGame.MouseY();
					}
				//if coursor in range and press space					
				if (myShape.IsAT(SwinGame.MousePosition()) && SwinGame.KeyTyped (KeyCode.vk_SPACE))
				{
					myShape.Color = SwinGame.RandomRGBColor (255);
				}



                //Draw onto the screen
                SwinGame.RefreshScreen(60);




            }
        }
    }
}