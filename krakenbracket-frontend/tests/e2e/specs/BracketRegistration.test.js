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
    cy.url().should("include", "/bracket-list");
  });
  it("Clicks on a bracket", () => {
    //cy.pause();
    cy.get(".bracket-link:first").click();
  });
  it("Makes sure you are on a specific bracket page", () => {
    cy.get("#title");
    cy.url().should("include", "bracket-view");
  });
  // it("Clicks on the 'Register!' button and takes you to the bracket registration page", () => {
  //   cy.contains("Register!").click();
  //   cy.contains("h1", "Signup");
  // });
});

describe("Register for a bracket", () => {
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
    cy.url().should("include", "/bracket-list");
  });
  it("Clicks on a bracket", () => {
    //cy.pause();
    cy.get(".bracket-link:first").click();
  });
  it("Makes sure you are on a specific bracket page", () => {
    cy.get("#title");
    cy.url().should("include", "bracket-view");
  });
  it("Makes sure that you are not logged in", () => {
    cy.contains("button", "Login");
  });
  it("Clicks on the 'login' button", () => {
    cy.contains("button", "Login").click();
  });
  it("Logs the user in", () => {
    const email = "user1@krakenbracket.com";
    const password = "Pass1";
    cy.get(".email-input").type(email);
    cy.get(".password-input").type(password);
    cy.contains("button", "Login").click();
  });
  it("Makes sure you are on a specific bracket page", () => {
    cy.get("#title");
    cy.url().should("include", "/bracket-view/1");
  });
  it("Clicks on the 'REGISTER!' button", () => {
    cy.contains("button", "Register!").click();
  });
  it("Registers the gamer", () => {
    const email = "user1@krakenbracket.com";
    const gamerTag = "GamerTag1";
    const gamerTagID = 1111;
    cy.get(".email-input").type(email);
    cy.get(".gamertag-input").type(gamerTag);
    cy.get(".gamertag-id-input").type(gamerTagID);
    cy.contains("button", "Register!").click();
  });
  it("Directs you back to bracket page and makes sure that you are registered", () => {
    cy.contains("p", "You are already registered for this event");
    cy.contains("button", "Unregister");
  });
});

describe("Unregister from a bracket", () => {
  it("Makes sure that you are registered", () => {
    cy.contains("button", "Unregister");
    cy.contains("p", "You are already registered for this event");
  });
  it("Clicks on the unregister button", () => {
    cy.contains("button", "Unregister").click();
  });
  it("Shows a popup window to confirm unregistration", () => {
    cy.contains("button", "I Accept");
  });
  it("Clicks on the button to confirm unregistration", () => {
    cy.contains("button", "I Accept").click();
  });
  it("Confirms that the user is unregistered from the bracket", () => {
    cy.contains("button", "Register!");
  });
});
