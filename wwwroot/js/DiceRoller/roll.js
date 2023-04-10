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
        console.log(this.dice)
        const rollName = document.createElement("div")
        rollName.classList = "rollName"
        rollName.textContent = this.name
        document.querySelector(".currRoll").append(rollName)

        const currRollCont = document.createElement("div")
        currRollCont.classList = "currRollCont"

        const diceImg = document.createElement("div")
        diceImg.classList = `diceImg`
        diceImg.textContent = `HIT : ${this.currentRoll[0].outcome}`
        console.log(this.currentRoll.filter(element => {
            return element.name != 20
        }).reduce((acc, cv) => acc + cv.outcome, 0))
        const x = document.createElement("div")
        x.classList = 'diceImg'
        x.textContent = `ATK :  ${this.currentRoll.filter(element => {
            return element.name != 20
        }).reduce((acc, cv) => acc + cv.outcome, 0)}`

        currRollCont.append(diceImg, x)
        document.querySelector(".currRoll").append(currRollCont)

        this.currentRoll.map(element => this.render(element.name, element.count, element.outcome))
    }

    render(name, count, outcome) {
        const currRollCont = document.createElement("div")
        currRollCont.classList = "currRollCont"

        const diceImg = document.createElement("div")
        diceImg.classList = `diceImg`
        diceImg.textContent = `D${name}`

        const x = document.createElement("div")
        x.classList = 'currRollText'
        x.textContent = `X `

        const countText = document.createElement("div")
        countText.classList = 'currRollText'
        countText.textContent = ` ${count} `

        const equals = document.createElement("div")
        equals.classList = 'currRollText'
        equals.textContent = `= `

        const minusButton = document.createElement("div")
        minusButton.classList = `currRollText`
        minusButton.textContent = ` ${outcome}`

        currRollCont.append(diceImg, x, countText, equals, minusButton)
        document.querySelector(".currRoll").append(currRollCont)
        return
    }
}