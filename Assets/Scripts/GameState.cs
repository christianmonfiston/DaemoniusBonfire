public enum GameState
{
	GAME_PLAY,
	PAUSED,
	DEAD
}
public class GameStateMachine
{
	public static GameState gameState = GameState.GAME_PLAY;
}