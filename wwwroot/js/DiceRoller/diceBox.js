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

        const rollButton = document.createElement("input")
        rollButton.classList = "rollButton"
        rollButton.type = "submit"
        rollButton.value = "Roll"

        const hitContainer = document.createElement("div")
        hitContainer.classList = "hitContainer"

        const hitModLabel = document.createElement("div")
        hitModLabel.classList = "hitModLabel"
        hitModLabel.textContent = "Hit Mod:"

        const hitMod = document.createElement("input")
        hitMod.classList = "hitMod"
        hitMod.type = "number"
        hitMod.value = 0

        const atkContainer = document.createElement("div")
        atkContainer.classList = "atkContainer"

        const atkModLabel = document.createElement("div")
        atkModLabel.classList = "atkModLabel"
        atkModLabel.textContent = "Atk Mod:"

        const atkMod = document.createElement("input")
        atkMod.classList = "atkMod"
        atkMod.type = "number"
        atkMod.value = 0

        hitContainer.append(hitModLabel, hitMod)
        atkContainer.append(atkModLabel, atkMod)
        rollButtonContainer.append(hitContainer, rollButton, atkContainer)
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