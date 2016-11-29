
using System.Windows.Forms;

public class MainMenu : Menu {
	
	public MainMenu() : base("Main Menu"){

    }

	public override void renderObjects() {
        elementList.Clear();
		elementList.Add(CreateUI.createButton(0, 1, 3, 1.25f, () => {
			GameInstance.setLevel(GameInstance.currentLevel);
		}, new GameFont("Continue", GameColor.RED)));
		
		elementList.Add(CreateUI.createButton(0, -0.5f, 3, 1.25f, () => {
			GameInstance.latestLevel = 2;
			GameInstance.setLevel(2);
		}, new GameFont("New Game", GameColor.RED)));
		
		elementList.Add(CreateUI.createButton(0, -2f, 3, 1.25f, () => {
			GameInstance.setLevel(1);
		}, new GameFont("Options", GameColor.RED)));
		elementList.Add(CreateUI.createButton(0, -3.5f, 3, 1.25f, () =>{
			SaveData.writeData();
            Application.Exit();
		}, new GameFont("Quit", GameColor.RED)));
		
		elementList.Add(new Text(0f, 5f, Alignment.CENTER, "Laser Amazer", GameColor.RED, 6f));
		elementList.Add(new Text(0f, 4f, Alignment.CENTER, "A FEDD educational computer game.", GameColor.ORANGE, 2f));
	}

}
