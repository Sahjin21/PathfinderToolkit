let announcementList = document.querySelector(".announceList")

let announcements = ["this", "that", "evenmore of this and that", "Now working: Bestiary, Ability, Actions, Afflictions, and Dice Roller"]

announcements.map(announcement => {
    let listItem = document.createElement("li")
    listItem.classList.add("announcementItem")
    listItem.textContent = announcement

    announcementList.appendChild(listItem)
})