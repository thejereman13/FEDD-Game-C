
using System.Windows.Forms;

public class GameComplete : Menu {
	
	public GameComplete() : base("Game Complete"){
	}

	public override void renderObjects() {
		elementList.Add(CreateUI.createButton(0, -2f, 6f, 1.5f, () =>{
			GameInstance.setLevel(0);
		}, new GameFont("Main Menu", GameColor.TEAL)));
		elementList.Add(CreateUI.createButton(0, -4f, 6f, 1.5f, () =>{
			SaveData.writeData();
            Application.Exit();
		}, new GameFont("Quit Game", GameColor.RED)));
		
		elementList.Add(new Text(0f, 3.5f, Alignment.CENTER, "Congratulations!", GameColor.ORANGE, 5f));
		elementList.Add(new Text(0f, 1.5f, Alignment.CENTER, "You've completed the game.", GameColor.YELLOW, 2.5f));
	}

}
