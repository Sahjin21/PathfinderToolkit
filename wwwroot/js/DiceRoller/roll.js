class Roll {
    constructor(name, dice) {
        this.name = name
        this.dice = dice
    }

    roll() {
        console.log(this.dice)
        const rollName = document.createElement("div")
        rollName.classList = "rollName"
        rollName.textContent = this.name
        document.querySelector(".currRoll").append(rollName)

        this.dice.filter(filElement => filElement.count != 0).map(element => {
            let outcome = Math.ceil(Math.random() * element.name) * element.count
            this.render(element.name, element.count, outcome)
        })


    }

    render(name, count, outcome) {
        const currRollCont = document.createElement("div")
        currRollCont.classList = "currRollCont"

        const diceImg = document.createElement("input")
        diceImg.classList = `diceImg`
        diceImg.value = `D${name}`

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