let announcementList = document.querySelector(".announceList")

let announcements = ["this", "that", "evenmore of this and that", "Now working: Bestiary, Ability, Actions, Afflictions, and Dice Roller"]

announcements.map(announcement => {
    let listItem = `<li class="announcementItem">${announcement}</li>`

    announcementList.appendChild(listItem)
})