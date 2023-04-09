class Dice {
    constructor(name) {
        this.name = name
        this.count = 0
    }

    render(name) {
        const diceCont = document.createElement("div")
        diceCont.classList = "diceCont"

        const addButton = document.createElement("input")
        addButton.classList = `diceImg d${name}Plus`
        addButton.type = "submit"
        addButton.value = `+D${name}`

        const txtBetweenButtons = document.createElement("input")
        txtBetweenButtons.classList = `dicetext d${name}Text`
        txtBetweenButtons.type = "number"
        txtBetweenButtons.value = 0

        const minusButton = document.createElement("input")
        minusButton.classList = `diceImg d${name}Minus`
        minusButton.type = "submit"
        minusButton.value = `-D${name}`

        diceCont.append(addButton, txtBetweenButtons, minusButton)
        return diceCont
    }
}