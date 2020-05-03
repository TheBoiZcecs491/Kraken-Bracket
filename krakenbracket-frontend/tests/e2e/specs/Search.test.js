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
        cy.url().should("include", "/search");
    });
    it("Types in search", () => {
        //cy.pause();
        const search = "EVO";
        cy.get(".search-input").type(search);
    });
    it("Performs search", () => {
        cy.contains("button", "Search").click();
    });
    it("Checks search results", () => {
        cy.get('tbody > :nth-child(1) > :nth-child(1)').contains("EVO")
    })
});

describe("Sort By End Date Descending", () => {
    it("Visits the app root url", () => {
        cy.get('[aria-label="End Date: Not sorted. Activate to sort ascending."]').click();
        cy.get(".active").click();
    });
    it("Visits the app root url", () => {
        cy.get('tbody > :nth-child(1) > :nth-child(5)').contains("2022");
        cy.pause();
        });
});

describe("Clear Search Results", () => {
    it("Clears search input", () => {
        cy.get(".col-sm-6 > .v-input > .v-input__control > .v-input__slot > :nth-child(2) > .v-input__icon > .v-icon").click();
    });
    it("Updates search model",  () => {
        cy.contains("button", "Search").click();
    });
});

describe("Search For Events Containing 'EVO'", () => {
    it("Change selector to Events", () => {
        cy.get('.search-type').select("Events")
    });
    it("Types in search", () => {
        const search = "EVO";
        cy.get(".search-input").type(search);
    });
    it("Performs search", () => {
        cy.contains("button", "Search").click();
    });
    it("Checks search results", () => {
        cy.get('td').contains("No data available");
    });
});

describe("Change Search Request To 'Year 2020'", () => {
    it("Clears search input", () => {
        cy.get(".col-sm-6 > .v-input > .v-input__control > .v-input__slot > :nth-child(2) > .v-input__icon > .v-icon").click();
    });
    it("Types in search", () => {
        const search = "Year 2020";
        cy.get(".search-input").type(search);
    });
    it("Performs search", () => {
        cy.contains("button", "Search").click();
    });
    it("Checks search results", () => {
        cy.get('tbody > :nth-child(1) > :nth-child(1)').contains("Year 2020")
    })
});

describe("Search For Multiple Gamers", () => {
    it("Clears search input", () => {
        cy.get(".col-sm-6 > .v-input > .v-input__control > .v-input__slot > :nth-child(2) > .v-input__icon > .v-icon").click();
    });
    it("Change selector to Gamers", () => {
        cy.get('.search-type').select("Gamers")
    });
    it("Types in search for 'gamer'", () => {
        const search = "gamer";
        cy.get(".search-input").type(search);
    });
    it("Performs search", () => {
        cy.contains("button", "Search").click();
    });
    it("Checks search results", () => {
        cy.get('tbody > :nth-child(1) > :nth-child(1)').contains("gamer");
    })
});

describe("Filter For A Specific Gamer", () => {
    it("Types in filter text", () => {
        const filter = "gamer9";
        cy.get('#input-22').type(filter);
    });
    it("Checks filter results", () => {
        cy.get('tbody > tr > .text-start').contains("gamer9");
        });
});

//pagination
