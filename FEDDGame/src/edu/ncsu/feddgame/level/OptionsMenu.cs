
using System;
using System.Collections.Generic;

public class OptionsMenu : Menu {
	
	public List<UIElement> gameList = new List<UIElement>();
	public List<UIElement> graphicsList = new List<UIElement>();
	private int display = 0;
	private bool levelComplete = true, showTimer = true;
	private Button complete, timer;
	private Dropdown multisampling;
	
	public OptionsMenu() : base("Options Menu") {
	}

	public override void renderObjects() {
		elementList.Clear();
		gameList.Clear();
		graphicsList.Clear();
		elementList.Add(CreateUI.createButton(-2, 6, 3, 1.5f, () =>{
			display = 0;
		}, new GameFont("Game", GameColor.TEAL)));
		elementList.Add(CreateUI.createButton(2, 6, 3, 1.5f, () =>{
			display = 1;
		}, new GameFont("Graphics", GameColor.TEAL)));
		elementList.Add(CreateUI.createButton(0, -6, 6, 1.5f, () =>{
			SaveData.writeData();
			GameInstance.setLevel(0);
		}, new GameFont("Return to Main Menu", GameColor.TEAL)));
		
		gameList.Add(new Text(0f, 4f, Alignment.CENTER, "Game Options", GameColor.ORANGE, 1.5f));
		gameList.Add(new Text(-9f, 1f, "Show Level Complete -", GameColor.BLUE, 1.2f));
		gameList.Add(new Text(-5.4f, 0f, "Show Timer -", GameColor.BLUE, 1.2f));
		complete = CreateUI.createButton(2f, 2.25f, 3, .8f, () => {
			levelComplete = !levelComplete;
			if (levelComplete){
				complete.setLabel(new GameFont(" Yes ", GameColor.YELLOW));
			}else{
				complete.setLabel(new GameFont(" No ", GameColor.YELLOW));
			}
			GameInstance.levelCompleteDialogue = levelComplete;
		}, new GameFont(GameInstance.levelCompleteDialogue ? " Yes " : " No ", GameColor.YELLOW));
		gameList.Add(complete);
		timer = CreateUI.createButton(2f, 0.25f, 3, .8f, () => {
			showTimer = !showTimer;
			if (showTimer){
				timer.setLabel(new GameFont(" Yes ", GameColor.YELLOW));
			}else{
				timer.setLabel(new GameFont(" No ", GameColor.YELLOW));
			}
			GameInstance.showTimer = showTimer;
		}, new GameFont(GameInstance.showTimer ? " Yes " : " No ", GameColor.YELLOW));
		gameList.Add(timer);
		
		graphicsList.Add(new Text(0f, 4f, Alignment.CENTER, "Graphics Options", GameColor.ORANGE, 1.5f));
		graphicsList.Add(new Text(-9f, 1f, "Multisampling Level -", GameColor.BLUE, 1.2f));
		multisampling = CreateUI.createDropdown(2f, 2.25f, 2.5f, .8f, new GameFont(" " + GameInstance.samplingLevel.ToString(), GameColor.YELLOW), new GameFont[]{
				new GameFont(" 0", GameColor.YELLOW),
				new GameFont(" 1", GameColor.YELLOW),
				new GameFont(" 2", GameColor.YELLOW),
				new GameFont(" 3", GameColor.YELLOW),
				new GameFont(" 4", GameColor.YELLOW)
		}, new Action[]{
				() => {
					GameInstance.samplingLevel = 0;
					multisampling.setLabel(new GameFont(" " + GameInstance.samplingLevel.ToString(), GameColor.YELLOW));
				},
				() => {
					GameInstance.samplingLevel = 1;
					multisampling.setLabel(new GameFont(" " + GameInstance.samplingLevel.ToString(), GameColor.YELLOW));
				},
				() => {
					GameInstance.samplingLevel = 2;
					multisampling.setLabel(new GameFont(" " + GameInstance.samplingLevel.ToString(), GameColor.YELLOW));
				},
				() => {
					GameInstance.samplingLevel = 3;
					multisampling.setLabel(new GameFont(" " + GameInstance.samplingLevel.ToString(), GameColor.YELLOW));
				},
				() => {
					GameInstance.samplingLevel = 4;
					multisampling.setLabel(new GameFont(" " + GameInstance.samplingLevel.ToString(), GameColor.YELLOW));
				}
		});
		graphicsList.Add(multisampling);
	}
	
	public override void renderLoop(){
		base.renderLoop();
		if (display == 0){
			foreach (UIElement e in gameList){
				e.render();
			}
		}else if(display == 1){
			foreach (UIElement e in graphicsList)
				e.render();
		}
	}
	
	public override void checkClick(float mouseX, float mouseY){
		base.checkClick(mouseX, mouseY);
		foreach (UIElement e in gameList){
			if (e is IClickable){
				((IClickable)e).checkClick(mouseX, mouseY);
			}
		}
		foreach (UIElement e in graphicsList){
			if (e is IClickable){
				((IClickable)e).checkClick(mouseX, mouseY);
			}
		}
	}

}
