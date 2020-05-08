describe("Search For Brackets Containing 'EVO'", () => {
  it("Visits the app root url", () => {
    cy.visit("http://localhost:8080/#/");
    cy.contains("h1", "Welcome to Kraken Bracket");
  });
  it("Clicks on the Search router link", () => {
    cy.contains("Search").click();
  });
  it("Makes sure you are on the search view", () => {
    cy.url().should("include", "/search");
  });
  it("Types in search", () => {
    const search = "EVO";
    cy.get(".search-input").type(search);
  });
  it("Performs search", () => {
    cy.contains("button", "Search").click();
  });
  it("Checks search results", () => {
    cy.get("tbody > :nth-child(1) > :nth-child(1)").contains("EVO");
    //cy.pause();
  });
});

describe("Sort By End Date Descending", () => {
  it("Sorts by end date ascending", () => {
    cy.get(
      '[aria-label="End Date: Not sorted. Activate to sort ascending."]'
    ).click();
    cy.get(".active").click();
  });
  it("Sorts by end date descending", () => {
    cy.get("tbody > :nth-child(1) > :nth-child(5)").contains("2022");
    //cy.pause();
  });
});

describe("Clear Search Results", () => {
  it("Clears search input", () => {
    cy.get(
      ".col-sm-6 > .v-input > .v-input__control > .v-input__slot > :nth-child(2) > .v-input__icon > .v-icon"
    ).click();
  });
  it("Updates search model", () => {
    cy.contains("button", "Search").click();
    //cy.pause();
  });
});

describe("Search For Events Containing 'EVO'", () => {
  it("Change selector to Events", () => {
    cy.get(".search-type").select("Events");
  });
  it("Types in search", () => {
    const search = "EVO";
    cy.get(".search-input").type(search);
  });
  it("Performs search", () => {
    cy.contains("button", "Search").click();
  });
  it("Checks search results", () => {
    cy.get("td").contains("No data available");
    //cy.pause();
  });
});

describe("Change Search Request To 'Year 2020'", () => {
  it("Clears search input", () => {
    cy.get(
      ".col-sm-6 > .v-input > .v-input__control > .v-input__slot > :nth-child(2) > .v-input__icon > .v-icon"
    ).click();
  });
  it("Types in search", () => {
    const search = "Year 2020";
    cy.get(".search-input").type(search);
  });
  it("Performs search", () => {
    cy.contains("button", "Search").click();
  });
  it("Checks search results", () => {
    cy.get("tbody > :nth-child(1) > :nth-child(1)").contains("Year 2020");
    //cy.pause();
  });
});

describe("Search For Multiple Gamers", () => {
  it("Clears search input", () => {
    cy.get(
      ".col-sm-6 > .v-input > .v-input__control > .v-input__slot > :nth-child(2) > .v-input__icon > .v-icon"
    ).click();
  });
  it("Change selector to Gamers", () => {
    cy.get(".search-type").select("Gamers");
  });
  it("Types in search for 'gamer'", () => {
    const search = "gamer";
    cy.get(".search-input").type(search);
  });
  it("Performs search", () => {
    cy.contains("button", "Search").click();
  });
  it("Checks search results", () => {
    cy.get("tbody > :nth-child(1) > :nth-child(1)").contains("gamer");
    //cy.pause();
  });
});

describe("Paginate Forward And Backwards", () => {
  it("Paginates forward", () => {
    cy.get(
      ".v-data-footer__icons-after > .v-btn > .v-btn__content > .v-icon"
    ).click();
  });
  it("Paginates backward", () => {
    cy.get(
      ".v-data-footer__icons-before > .v-btn > .v-btn__content > .v-icon"
    ).click();
    //cy.pause();
  });
});

describe("Filter For A Specific Gamer", () => {
  it("Types in filter text", () => {
    const filter = "gamer9";
    cy.get("#input-22").type(filter);
  });
  it("Checks filter results", () => {
    cy.get("tbody > tr > .text-start").contains("gamer9");
    //cy.pause();
  });
});

describe("Filter For A Invalid Gamer", () => {
  it("Clears filter text", () => {
    cy.get(
      ".v-card__title > .v-input > .v-input__control > .v-input__slot > .v-input__append-inner > .v-input__icon > .v-icon"
    ).click();
  });
  it("Types in filter text", () => {
    const filter = "invalid";
    cy.get("#input-22").type(filter);
  });
  it("Checks filter results", () => {
    cy.get("td").contains("No matching records found");
    //cy.pause();
  });
});
