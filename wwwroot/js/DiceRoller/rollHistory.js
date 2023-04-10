class RollHistory{
    constructor() {
        this.rollHistory = []
    }


    seed(roll) {
        if (this.rollHistory.length > 10) {
            this.rollHistory.shift()
            this.rollHistory.push({ name: roll.name, roll: roll.currentRoll })
        } else {
            this.rollHistory.push({ name: roll.name, roll: roll.currentRoll })
        }
        console.log(this.rollHistory, "check")
        this.renderHistory()
    }

    renderHistory() {
        const rollHist = document.querySelector(".rollHist")

        //     while (rollHist.firstChild) {
        //         rollHist.removeChild(rollHist.firstChild);
        // }

        console.log(this.rollHistory,"check")
        this.rollHistory.map(element => {

            const nameAndRollCont = document.createElement("div")
            nameAndRollCont.classList = "nameAndRollCont"

            const rollName = document.createElement("div")
            rollName.classList = "rollName currRollCont"
            rollName.textContent = element.name

            const currRollCont = document.createElement("div")
            currRollCont.classList = "currRollCont"

            const hitBlockDisplay = document.createElement("div")
            hitBlockDisplay.classList = `HITDiceImg`
            hitBlockDisplay.textContent = `HIT : ${element.roll[0].outcome + Number(document.querySelector(".hitMod").value)}`

            const atkBlockDisplay = document.createElement("div")
            atkBlockDisplay.classList = 'ATKDiceImg'
            atkBlockDisplay.textContent = `ATK :  ${element.roll.filter(filElement => {
                return filElement.name != 20
            }).reduce((acc, cv) => acc + cv.outcome, 0) + Number(document.querySelector(".atkMod").value)}`

            currRollCont.append(hitBlockDisplay, atkBlockDisplay)
            nameAndRollCont.append(rollName, currRollCont)
            rollHist.append(nameAndRollCont)


        })

    }
}