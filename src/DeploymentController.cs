
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The DeploymentController controls the players actions
/// during the deployment phase.
/// </summary>
static class DeploymentController
{
	private const int SHIPS_TOP = 98;
	private const int SHIPS_LEFT = 20;
	private const int SHIPS_HEIGHT = 90;

	private const int SHIPS_WIDTH = 300;
	private const int TOP_BUTTONS_TOP = 72;

	private const int TOP_BUTTONS_HEIGHT = 46;
	private const int PLAY_BUTTON_LEFT = 693;

	private const int PLAY_BUTTON_WIDTH = 80;
	private const int UP_DOWN_BUTTON_LEFT = 410;

	private const int LEFT_RIGHT_BUTTON_LEFT = 350;

	private const int RANDOM_BUTTON_LEFT = 488;
	private const int RANDOM_BUTTON_WIDTH = 51;

	private const int SOUND_BUTTON = 550;
	private const int SOUND_BUTTON_WIDTH = 51;

	private const int MUTE_SOUND_BUTTON = 550;
	private const int MUTE_SOUND_BUTTON_WIDTH = 51;

	private const int BGM_BUTTON = 610;
	private const int BGM_BUTTON_WIDTH = 51;

	private const int Mute_BGM_BUTTON = 610;
	private const int Mute_BGM_BUTTON_WIDTH = 51;

	private const int DIR_BUTTONS_WIDTH = 47;

	private const int TEXT_OFFSET = 5;
	private static Direction _currentDirection = Direction.UpDown;

	public static MuteStatus _currentBGMStatus = MuteStatus.unmute;
	public static MuteStatus _currentSEStatus = MuteStatus.unmute;


	private static ShipName _selectedShip = ShipName.Tug;
	/// <summary>
	/// Handles user input for the Deployment phase of the game.
	/// </summary>
	/// <remarks>
	/// Involves selecting the ships, deloying ships, changing the direction
	/// of the ships to add, randomising deployment, end then ending
	/// deployment
	/// </remarks>
	public static void HandleDeploymentInput()
	{
		if (SwinGame.KeyTyped (KeyCode.vk_ESCAPE))
		{
//			AddNewState(GameState.ViewingGameMenu);
			GameController.AddNewState (GameState.ViewingGameMenu);
		}

		if (SwinGame.KeyTyped (KeyCode.vk_UP) | SwinGame.KeyTyped (KeyCode.vk_DOWN))
		{
			_currentDirection = Direction.UpDown;
		}
		if (SwinGame.KeyTyped (KeyCode.vk_LEFT) | SwinGame.KeyTyped (KeyCode.vk_RIGHT))
		{
			_currentDirection = Direction.LeftRight;
		}

		if (SwinGame.KeyTyped (KeyCode.vk_r))
		{
//			HumanPlayer.RandomizeDeployment();
			GameController.HumanPlayer.RandomizeDeployment ();
		}

		if (SwinGame.KeyTyped (KeyCode.vk_m))
		{
			//Audio.PauseMusic();
			//_currentBGMStatus = MuteStatus.mute;
			//_currentSEStatus = MuteStatus.unmute;

			if (_currentBGMStatus == MuteStatus.mute)
			{
				Audio.ResumeMusic ();
				_currentBGMStatus = MuteStatus.unmute;
			}
			else
			{
				Audio.PauseMusic ();
				_currentBGMStatus = MuteStatus.mute;
			}
		}

		if (SwinGame.KeyTyped (KeyCode.vk_s))
		{
			//Audio.ResumeMusic ();
			//_currentSEStatus = MuteStatus.mute;
			//_currentSEStatus = MuteStatus.unmute;

			//Audio.PlaySoundEffect(GameResources.GameSound("Error"));
			//Audio.PlaySoundEffect(GameResources.GameSound("Hit"));
			//Audio.PlaySoundEffect(GameResources.GameSound("Sink"));
			//Audio.PlaySoundEffect(GameResources.GameSound("Siren"));
			//Audio.PlaySoundEffect(GameResources.GameSound("Miss"));
			//Audio.PlaySoundEffect(GameResources.GameSound("Winner"));
			//Audio.PlaySoundEffect(GameResources.GameSound("Lose"));

			if (_currentSEStatus == MuteStatus.mute)
			{
				Audio.PlaySoundEffect (GameResources.GameSound("boop"));
				_currentSEStatus = MuteStatus.unmute;
			}
			else
			{
				_currentSEStatus = MuteStatus.mute;
			}


		}

		if (SwinGame.MouseClicked (MouseButton.LeftButton))
		{
			ShipName selected = default(ShipName);
			selected = GetShipMouseIsOver ();
			if (selected != ShipName.None)
			{
				_selectedShip = selected;
			}
			else
			{
				DoDeployClick ();
			}

//			if (HumanPlayer.ReadyToDeploy & IsMouseInRectangle(PLAY_BUTTON_LEFT, TOP_BUTTONS_TOP, PLAY_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT)) {
			if (GameController.HumanPlayer.ReadyToDeploy & UtilityFunctions.IsMouseInRectangle (PLAY_BUTTON_LEFT, TOP_BUTTONS_TOP, PLAY_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT))
			{
//				EndDeployment();
				GameController.EndDeployment ();
//			} else if (IsMouseInRectangle(UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP, DIR_BUTTONS_WIDTH, TOP_BUTTONS_HEIGHT)) {
			}
			else if (UtilityFunctions.IsMouseInRectangle (UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP, DIR_BUTTONS_WIDTH, TOP_BUTTONS_HEIGHT))
			{
				// fix from leftright to updown
				_currentDirection = Direction.UpDown;
//			} else if (IsMouseInRectangle(LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP, DIR_BUTTONS_WIDTH, TOP_BUTTONS_HEIGHT)) {
			}
			else if (UtilityFunctions.IsMouseInRectangle (LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP, DIR_BUTTONS_WIDTH, TOP_BUTTONS_HEIGHT))
			{
				_currentDirection = Direction.LeftRight;
//			} else if (IsMouseInRectangle(RANDOM_BUTTON_LEFT, TOP_BUTTONS_TOP, RANDOM_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT)) {
			}
			else if (UtilityFunctions.IsMouseInRectangle (RANDOM_BUTTON_LEFT, TOP_BUTTONS_TOP, RANDOM_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT))
			{
//				HumanPlayer.RandomizeDeployment();				
				GameController.HumanPlayer.RandomizeDeployment ();

			}else if (UtilityFunctions.IsMouseInRectangle (Mute_BGM_BUTTON, TOP_BUTTONS_TOP, BGM_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT)&_currentBGMStatus == MuteStatus.unmute)
			{
				Audio.PauseMusic();
				_currentBGMStatus = MuteStatus.mute;
			}
			else if(UtilityFunctions.IsMouseInRectangle (BGM_BUTTON, TOP_BUTTONS_TOP, BGM_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT)&_currentBGMStatus == MuteStatus.mute)
			{
				Audio.ResumeMusic ();
				_currentBGMStatus = MuteStatus.unmute;
			}else if(UtilityFunctions.IsMouseInRectangle (MUTE_SOUND_BUTTON, TOP_BUTTONS_TOP, BGM_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT)&_currentSEStatus == MuteStatus.unmute)
			{
				
				_currentSEStatus = MuteStatus.mute;
			}else if(UtilityFunctions.IsMouseInRectangle (SOUND_BUTTON, TOP_BUTTONS_TOP, BGM_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT)&_currentSEStatus == MuteStatus.mute)
			{
				Audio.PlaySoundEffect (GameResources.GameSound("boop"));
				_currentSEStatus = MuteStatus.unmute;
			}


//			else if (UtilityFunctions.IsMouseInRectangle (MUTE_SOUND_BUTTON, TOP_BUTTONS_TOP, MUTE_SOUND_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT)&_currentSEStatus == MuteStatus.mute)
//			{
//				Audio.StopSoundEffect (GameResources.GameSound ("Miss"));
//				Audio.StopSoundEffect(GameResources.GameSound("Error"));
//				Audio.StopSoundEffect(GameResources.GameSound("Hit"));
//				Audio.StopSoundEffect(GameResources.GameSound("Sink"));
//				Audio.StopSoundEffect(GameResources.GameSound("Siren"));
//				Audio.StopSoundEffect(GameResources.GameSound("Winner"));
//				Audio.StopSoundEffect(GameResources.GameSound("Lose"));
//			
//			}
//			else if(UtilityFunctions.IsMouseInRectangle (SOUND_BUTTON, TOP_BUTTONS_TOP, SOUND_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT))
//			{
////				Audio.PlaySoundEffect(GameResources.GameSound("Error"));
////				Audio.PlaySoundEffect(GameResources.GameSound("Hit"));
////				Audio.PlaySoundEffect(GameResources.GameSound("Sink"));
////				Audio.PlaySoundEffect(GameResources.GameSound("Siren"));
////				Audio.PlaySoundEffect(GameResources.GameSound("Miss"));
////				Audio.PlaySoundEffect(GameResources.GameSound("Winner"));
////				Audio.PlaySoundEffect(GameResources.GameSound("Lose"));
//
//				//Audio.LoadSoundEffectNamed ("", "");
//				Audio.LoadSoundEffectNamed("Error", "error.wav");
////				NewSound("Hit", "hit.wav");
////				NewSound("Sink", "sink.wav");
////				NewSound("Siren", "siren.wav");
////				NewSound("Miss", "watershot.wav");
////				NewSound("Winner", "winner.wav");
////				NewSound("Lose", "lose.wav");
//
//			}


//			else if (UtilityFunctions.IsMouseInRectangle (BGM_BUTTON, TOP_BUTTONS_TOP, BGM_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT))
//			{
//				Audio.StopMusic();
//				_currentBGMStatus = MuteStatus.mute;
//			}
//			else if(UtilityFunctions.IsMouseInRectangle (Mute_BGM_BUTTON, TOP_BUTTONS_TOP, BGM_BUTTON_WIDTH, TOP_BUTTONS_HEIGHT)&_currentBGMStatus == MuteStatus.mute)
//			{
//				Audio.PlayMusic (GameResources.GameMusic ("Background"));
//				_currentBGMStatus = MuteStatus.unmute;
//			}
	
		}
	}
	/// <summary>
	/// The user has clicked somewhere on the screen, check if its is a deployment and deploy
	/// the current ship if that is the case.
	/// </summary>
	/// <remarks>
	/// If the click is in the grid it deploys to the selected location
	/// with the indicated direction
	/// </remarks>
	private static void DoDeployClick()
	{
		Point2D mouse = default(Point2D);

		mouse = SwinGame.MousePosition();

		//Calculate the row/col clicked
		int row = 0;
		int col = 0;
//		row = Convert.ToInt32(Math.Floor((mouse.Y) / (CELL_HEIGHT + CELL_GAP)));
										// Fixed Ship location Error
		row = Convert.ToInt32(Math.Floor((mouse.Y - 120) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));

//		col = Convert.ToInt32(Math.Floor((mouse.X - FIELD_LEFT) / (CELL_WIDTH + CELL_GAP)));
		col = Convert.ToInt32(Math.Floor((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

//		if (row >= 0 & row < HumanPlayer.PlayerGrid.Height) {
		if (row >= 0 & row < GameController.HumanPlayer.PlayerGrid.Height) {
//			if (col >= 0 & col < HumanPlayer.PlayerGrid.Width) {
			if (col >= 0 & col < GameController.HumanPlayer.PlayerGrid.Width) {
				//if in the area try to deploy
				try {
//					HumanPlayer.PlayerGrid.MoveShip(row, col, _selectedShip, _currentDirection);
					GameController.HumanPlayer.PlayerGrid.MoveShip(row, col, _selectedShip, _currentDirection);
				} catch (Exception ex) {
//					Audio.PlaySoundEffect(GameSound("Error"));
					Audio.PlaySoundEffect(GameResources.GameSound("Error"));
//					Message = ex.Message;
					UtilityFunctions.Message = ex.Message;
				}
			}
		}
	}

	/// <summary>
	/// Draws the deployment screen showing the field and the ships
	/// that the player can deploy.
	/// </summary>
	public static void DrawDeployment()
	{
//		DrawField(HumanPlayer.PlayerGrid, HumanPlayer, true);
		UtilityFunctions.DrawField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer, true);

		//Draw the Left/Right and Up/Down buttons
		if (_currentDirection == Direction.LeftRight) {
//			SwinGame.DrawBitmap(GameImage("LeftRightButton"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP);
			SwinGame.DrawBitmap(GameResources.GameImage("LeftRightButton"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP);
			//SwinGame.DrawText("U/D", Color.Gray, GameFont("Menu"), UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP)
			//SwinGame.DrawText("L/R", Color.White, GameFont("Menu"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP)
		} else {
//			SwinGame.DrawBitmap(GameImage("UpDownButton"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP);
			SwinGame.DrawBitmap(GameResources.GameImage("UpDownButton"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP);
			//SwinGame.DrawText("U/D", Color.White, GameFont("Menu"), UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP)
			//SwinGame.DrawText("L/R", Color.Gray, GameFont("Menu"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP)
		}

		if (_currentSEStatus == MuteStatus.unmute) {
			//			SwinGame.DrawBitmap(GameImage("LeftRightButton"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP);
			SwinGame.DrawBitmap(GameResources.GameImage("SoundButton"), MUTE_SOUND_BUTTON, TOP_BUTTONS_TOP);
			_currentSEStatus = MuteStatus.unmute;
			//SwinGame.DrawText("U/D", Color.Gray, GameFont("Menu"), UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP)
			//SwinGame.DrawText("L/R", Color.White, GameFont("Menu"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP)
		} else {
			//			SwinGame.DrawBitmap(GameImage("UpDownButton"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP);
			SwinGame.DrawBitmap(GameResources.GameImage("MuteSoundButton"), SOUND_BUTTON, TOP_BUTTONS_TOP);
			//SwinGame.DrawText("U/D", Color.White, GameFont("Menu"), UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP)
			//SwinGame.DrawText("L/R", Color.Gray, GameFont("Menu"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP)
		}

		if (_currentBGMStatus == MuteStatus.unmute) {
			//			SwinGame.DrawBitmap(GameImage("LeftRightButton"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP);
			SwinGame.DrawBitmap(GameResources.GameImage("BGMButton"), BGM_BUTTON, TOP_BUTTONS_TOP);
			_currentBGMStatus = MuteStatus.unmute;
			//SwinGame.DrawText("U/D", Color.Gray, GameFont("Menu"), UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP)
			//SwinGame.DrawText("L/R", Color.White, GameFont("Menu"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP)
		} else if (_currentBGMStatus == MuteStatus.mute) {
			//			SwinGame.DrawBitmap(GameImage("UpDownButton"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP);
			SwinGame.DrawBitmap(GameResources.GameImage("MuteBGMButton"), Mute_BGM_BUTTON, TOP_BUTTONS_TOP);
			//SwinGame.DrawText("U/D", Color.White, GameFont("Menu"), UP_DOWN_BUTTON_LEFT, TOP_BUTTONS_TOP)
			//SwinGame.DrawText("L/R", Color.Gray, GameFont("Menu"), LEFT_RIGHT_BUTTON_LEFT, TOP_BUTTONS_TOP)
		}

		//DrawShips
		foreach (ShipName sn in Enum.GetValues(typeof(ShipName))) {
			int i = 0;
//			i =  Conversion.Int(sn) - 1;
			i =  ((int) sn) - 1;
			if (i >= 0) {
				if (sn == _selectedShip) {
//					SwinGame.DrawBitmap(GameImage("SelectedShip"), SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT);
					SwinGame.DrawBitmap(GameResources.GameImage("SelectedShip"), SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT);
					//    SwinGame.FillRectangle(Color.LightBlue, SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)
					//Else
					//    SwinGame.FillRectangle(Color.Gray, SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)
				}

				//SwinGame.DrawRectangle(Color.Black, SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)
				//SwinGame.DrawText(sn.ToString(), Color.Black, GameFont("Courier"), SHIPS_LEFT + TEXT_OFFSET, SHIPS_TOP + i * SHIPS_HEIGHT)

			}
		}

//		if (HumanPlayer.ReadyToDeploy) {
		if (GameController.HumanPlayer.ReadyToDeploy) {
//			SwinGame.DrawBitmap(GameImage("PlayButton"), PLAY_BUTTON_LEFT, TOP_BUTTONS_TOP);
			SwinGame.DrawBitmap(GameResources.GameImage("PlayButton"), PLAY_BUTTON_LEFT, TOP_BUTTONS_TOP);
			//SwinGame.FillRectangle(Color.LightBlue, PLAY_BUTTON_LEFT, PLAY_BUTTON_TOP, PLAY_BUTTON_WIDTH, PLAY_BUTTON_HEIGHT)
			//SwinGame.DrawText("PLAY", Color.Black, GameFont("Courier"), PLAY_BUTTON_LEFT + TEXT_OFFSET, PLAY_BUTTON_TOP)
		}

//		SwinGame.DrawBitmap(GameImage("RandomButton"), RANDOM_BUTTON_LEFT, TOP_BUTTONS_TOP);
		SwinGame.DrawBitmap(GameResources.GameImage("RandomButton"), RANDOM_BUTTON_LEFT, TOP_BUTTONS_TOP);
		//SwinGame.DrawBitmap(GameResources.GameImage("MuteButton"), MUTE_BUTTON_LEFT, TOP_BUTTONS_TOP);
		//SwinGame.DrawBitmap(GameResources.GameImage("SoundButton"), SOUND_BUTTON_LEFT, TOP_BUTTONS_TOP);
//		DrawMessage();
		UtilityFunctions.DrawMessage ();
	}

	/// <summary>
	/// Gets the ship that the mouse is currently over in the selection panel.
	/// </summary>
	/// <returns>The ship selected or none</returns>
	private static ShipName GetShipMouseIsOver()

	{
		foreach (ShipName sn in Enum.GetValues(typeof(ShipName))) {
			int i = 0;
//			i = Conversion.Int(sn) - 1;
			i = ((int) sn) - 1;

//			if (IsMouseInRectangle(SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)) {
			if (UtilityFunctions.IsMouseInRectangle(SHIPS_LEFT, SHIPS_TOP + i * SHIPS_HEIGHT, SHIPS_WIDTH, SHIPS_HEIGHT)) {
				return sn;
			}
		}

		return ShipName.None;
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
