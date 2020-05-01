describe("My First Test", () => {
    it("Visits the app root url", () => {
      cy.visit("http://localhost:8080/#/");
      cy.contains("h1", "Welcome to Kraken Bracket");
    });
});

describe("Search For Brackets Containing 'EVO'", () => {
    it("Visits the app root url", () => {
        cy.visit("http://localhost:8080/#/");
        cy.contains("h1", "Welcome to Kraken Bracket");
    });
    it("Clicks on the Search router link", () => {
        //cy.pause();
        cy.contains("Search").click();
    });
    it("Makes sure you are on the search view", () => {
        //cy.pause();
        cy.url().should("include", "/search/:search");
    });
    it("Types in search", () => {
        //cy.pause();
        const search = "EVO";
        cy.get(".search-input").type(search);
    });
    it("Perform search", () => {
        cy.contains("button", "Search").click();
    });
});

describe("Clear Search Results", () => {
    it("Clear search input", () => {
        cy.get(".col-sm-6 > .v-input > .v-input__control > .v-input__slot > :nth-child(2) > .v-input__icon > .v-icon").click();
    });
    it("Update search model",  () => {
        cy.contains("button", "Search").click();
    });
});