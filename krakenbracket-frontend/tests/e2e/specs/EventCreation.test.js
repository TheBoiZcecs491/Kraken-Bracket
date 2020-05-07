// https://docs.cypress.io/api/introduction/api.html

describe("Create Event", () =>{
    it("Visit homepage ", () => {
        cy.visit("http://localhost:8080");
        cy.contains("h1", "Welcome to Kraken Bracket");

    });
    it("Click on Event page", () => {
        cy.contains("Event List").click();
    });
    it("Click on 'CREATE A NEW EVENT' button", () =>{
        cy.contains("Create a new Event").click();
    });
    it("At log in page, log in user", () =>{ 
        const email = "user3@krakenbracket.com";
        const password = "Pass3";
        cy.get(".email-input").type(email);
        cy.get(".password-input").type(password);
        cy.contains("button", "Login").click();
    });
    it("Click on 'CREATE A NEW EVENT' button", () =>{
        cy.contains("Create a new Event").click();
    });
    it("Fill out event form", () => {
        const EventName = "Street Fighter World Championship 2020";
        const Address = "1234 second street";
        const Description = "Official Street Fighter competition";
        cy.get(".EventName-input").type(EventName);
        cy.get(".EventAddress-input").type(Address);
        cy.get(".EventDescription-input").type(Description);
    });

    it("Select calendar start date", () => {
        cy.contains("Start Date").click({force:true});
        cy.get('tr').contains(7).click({force:true});
    });
    it("Select timer start timer",() =>{
        cy.contains("Start Time").click({force:true});
        cy.get("span").contains(5).click({force:true});
        cy.get("span").contains(30).click({force:true});
        cy.get("div").contains("PM").click({force:true});
    });

    it("Select calendar end date", () => {
        cy.contains("End Date").click();
        cy.get('tr').contains(9).click({force:true});
    });
    it("Select timer end timer",() =>{
        cy.contains("End Time").click({force:true});
        cy.get("span").contains(9).click({force:true});
        cy.get("span").contains(20).click({force:true});
        cy.get("div").contains("AM").click({force:true});
    });

    it("click out of the clock view", () =>{
        cy.contains("Create Event").click({force:true});
    })


}
)