// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const myDiceBox = new DiceBox()
myDiceBox.seed([20, 12, 10, 8, 6, 4])

console.log(myDiceBox)