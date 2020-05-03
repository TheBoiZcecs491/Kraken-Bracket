// https://docs.cypress.io/api/introduction/api.html

describe("Create a new account", () => {
    it("Visits the app root url", () => {
      cy.visit("http://localhost:8080/#/");
      cy.contains("h1", "Welcome to Kraken Bracket");
    });
    it("Clicks on register", () => {
      //cy.pause();
      cy.contains("register").click();
    });
    it("Check if we actually went to the user registration page", () => {
      //cy.pause();
      cy.contains("h1", "Register new user");
      cy.url().should("include", "/register");
    });
    it("Fills out the form", () => {
      //cy.pause();
      cy.contains("Email").type("docjohnson9001@fancypants.com")
      cy.contains("Password").type("wpAcWM&235xd");
      cy.contains("First Name").type("Johnson");
      cy.contains("Last Name").type("Daneeka");
      cy.contains("Register").click();
    });
    it("Makes sure you are on a specific bracket page", () => {
      cy.get("#title");
      cy.url().should("include", "bracket-view");
    });
  });
  