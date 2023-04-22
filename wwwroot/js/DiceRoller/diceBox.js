class DiceBox {
    constructor() {
        this.diceList = []
    }

    seed(diceData) {

        diceData.map(element => {
            const newDice = new Dice(element)
            this.diceList.push(newDice)
        })

        this.render()
        return this.diceList
    }

    addDice(diceType) {
        this.diceList.map(dice => {
            if (dice.name === +diceType) {
                dice.count += 1
            }
        })

    }

    subtractDice(diceType) {
        this.diceList.map(dice => {
            if (dice.name === +diceType) {
                dice.count -= 1
            }
        })

    }

    updateDiceText(diceType) {
        let dicetext = document.querySelector(`.d${diceType}Text`)
        this.diceList.map(dice => {
            if (dice.name === +diceType) {
                dicetext.value = dice.count
            }
        })
    }

    render() {
        const roll_container = document.querySelector(".roll_container")

        this.diceList.map(dice => {

            const diceItem = dice.render(dice.name)
            roll_container.append(diceItem)

        })

        const rollButtonContainer = document.createElement("div")
        rollButtonContainer.classList = "diceCont"

        let rollHtml = `
            <div class="hitContainer">
                <div class="hitModLabel">Hit Mod:</div>
                <input class="hitMod" type="number">
            </div>
            <input class="rollButton" type="submit" value="Roll">
            <div class="atkContainer">
                <div class="atkModLabel">Atk Mod:</div>
                <input class="atkMod" type="number">
            </div>`

        rollButtonContainer.innerHTML = rollHtml
        roll_container.append(rollButtonContainer)

        const rollNameContainer = document.createElement("div")
        rollNameContainer.classList = "rollNameContainer"

        const rollNameLabel = document.createElement("div")
        rollNameLabel.classList = "rollNameLabel"
        rollNameLabel.textContent = "Roll Name: "

        const rollNameInput = document.createElement("input")
        rollNameInput.classList = "rollNameInput"
        rollNameInput.type = "text"
        rollNameInput.value = "Unnamed"

        rollNameContainer.append(rollNameLabel, rollNameInput)
        roll_container.append(rollNameContainer)
    }
}