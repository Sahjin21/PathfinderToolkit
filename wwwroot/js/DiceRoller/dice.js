class Dice {
    constructor(name) {
        this.name = name
        this.count = 0
    }

    render(name) {
        const diceCont = document.createElement("div")
        diceCont.classList = "diceCont"

        const html = `
        <input type="submit" class="diceImg ${name}_Plus" value="+D${name}"></input>
        <input type="number" class="dicetext d${name}Text" value= 0></input>
        <input type="submit" class="diceImg ${name}_Minus" value="-D${name}"></input>
        `

        diceCont.innerHTML = html;
        return diceCont
    }
}