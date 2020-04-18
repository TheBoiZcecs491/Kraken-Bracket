// https://docs.cypress.io/api/introduction/api.html

describe("My First Test", () => {
  it("Visits the app root url", () => {
    cy.visit("http://localhost:8080/#/");
    cy.contains("h1", "Welcome to Kraken Bracket");
  });
});

describe("Viewing specific brackets", () => {
  it("Visits the app root url", () => {
    cy.visit("http://localhost:8080/#/");
    cy.contains("h1", "Welcome to Kraken Bracket");
  });
  it("Clicks on the Bracket List router link", () => {
    //cy.pause();
    cy.contains("Bracket List").click();
  });
  it("Makes sure you are on the bracket listings", () => {
    //cy.pause();
    cy.contains("h1", "Bracket Listings");
    cy.url().should('include', '/bracket-list')
  });
  it("Clicks on a bracket", () => {
    //cy.pause();
    cy.get(".bracket-card:first").click();
  });
  it("Makes sure you are on a specific bracket page", () => {
    cy.get("#title");
    cy.contains("button", "Register!");
    cy.url().should("include", "bracket-view");
  });
});
