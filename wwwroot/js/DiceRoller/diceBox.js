class DiceBox {
    constructor() {
        this.diceList = []
    }

    seed(diceData) {

        diceData.map(element => {
            const newDice = new Dice(element, 0)
            this.diceList.push(newDice)
        })

        console.log(this.diceList)
        this.render()
        return this.diceList
    }

    render() {
        const roll_container = document.querySelector(".roll_container")

        this.diceList.map(dice => {

            const diceItem = dice.render(dice.name)
            roll_container.append(diceItem)

            let addButton = document.querySelector(`.d${dice.name}Plus`)
            let minusButton = document.querySelector(`.d${dice.name}Minus`)
            let dicetext = document.querySelector(`.d${dice.name}Text`)
            addButton.addEventListener("click", function () {
                dice.count += 1
                dicetext.value = dice.count

            })

            minusButton.addEventListener("click", function () {
                if (dice.count > 0) {
                    dice.count -= 1
                    dicetext.value = dice.count
                }

            })
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
    }
}