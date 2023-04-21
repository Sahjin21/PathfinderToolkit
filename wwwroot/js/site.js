// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const myDiceBox = new DiceBox()
const rollHistory = new RollHistory()
myDiceBox.seed([20, 12, 10, 8, 6, 4])

const rollButton = document.querySelector(".rollButton")
const rollContainer = document.querySelector(".roll_container")

rollButton.addEventListener("click", function () {

    let newRoll = new Roll(document.querySelector(".rollNameInput").value, myDiceBox.diceList)
    rollHistory.seed(newRoll)
})

//this is for the plus and mins buttons for each dice in the roller
rollContainer.addEventListener("click", function (e) {
    let classArray = Array.from(e.target.classList)
    let diceType = classArray[1].split("_")[0]
    let buttonType = classArray[1].split("_")[1]

    if (buttonType === "Plus") {
        myDiceBox.addDice(diceType)
    } else if (buttonType === "Minus") {
        myDiceBox.subtractDice(diceType)
    }
    myDiceBox.updateDiceText(diceType)
})