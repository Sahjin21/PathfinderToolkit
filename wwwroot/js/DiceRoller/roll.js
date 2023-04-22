class Roll {
    constructor(name, dice) {
        this.name = name
        this.dice = dice
        this.currentRoll = []
        this.roll()
    }

    roll() {

        this.dice.filter(filElement => filElement.count != 0).map(element => {
            let outcome = Math.ceil(Math.random() * element.name) * element.count
            this.currentRoll.push({ name: element.name, count: element.count, outcome: outcome })
        })


        this.renderRoll()
    }

    renderRoll() {
        const currRoll = document.querySelector(".currRoll")
        const hitPlusMod = this.currentRoll[0].outcome + Number(document.querySelector(".hitMod").value)
        const atkPlusMod = this.currentRoll.filter(element => {
            return element.name != 20
        }).reduce((acc, cv) => acc + cv.outcome, 0) + Number(document.querySelector(".atkMod").value)

        if (document.querySelector(".currRollCont")) {
            while (document.querySelector(".currRollCont")) {
                document.querySelector(".currRollCont").remove();
            }
        }

        let html = `<div class="nameAndRollCont">
                        <div class="rollName currRollCont">${this.name}</div>
                        <div class="currRollCont">
                            <div class="HITDiceImg">HIT : ${hitPlusMod}</div>
                            <div class="ATKDiceImg">ATK : ${atkPlusMod}</div>
                        </div>
                    </div>`

        currRoll.innerHTML = html

        this.currentRoll.map(element => this.render(element.name, element.count, element.outcome))

    }

    render(name, count, outcome) {
        const currRollCont = document.createElement("div")
        currRollCont.classList = "currRollCont"

        let html = `
                    <div class="diceImg">D${name}</div>
                    <div class="currRollText">X </div>
                    <div class="currRollText"> ${count} </div>
                    <div class="currRollText">= </div>
                    <div class="currRollText"> ${outcome}</div>
                    `

        currRollCont.innerHTML = html
        document.querySelector(".currRoll").append(currRollCont)
        return currRollCont
    }
}