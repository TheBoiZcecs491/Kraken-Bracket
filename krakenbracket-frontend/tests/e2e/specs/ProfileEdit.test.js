describe("My First Test", () => {
    it("Visits the app root url", () => {
      cy.visit("http://localhost:8080/#/");
      cy.contains("h1", "Welcome to Kraken Bracket");
    });
  });

describe("View the profile", () => {
    it("Visits the app root url", () => {
      cy.visit("http://localhost:8080/#/");
      cy.contains("h1", "Welcome to Kraken Bracket");
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
    it("is now on the home page", () => {
        cy.contains("h1", "Welcome to Kraken Bracket");
    });
    it("clicks on profile", () => {
        cy.contains("button", "profile").click();
    });
    it("sees its profile and the edit button", () => {
        cy.contains("h1", "Their gamer tag goes here");
        cy.contains("button", "edit");
    });
  });

  describe("edit the profile gamer tag", () => {
    it("Visits the app root url", () => {
      cy.visit("http://localhost:8080/#/");
      cy.contains("h1", "Welcome to Kraken Bracket");
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
    it("is now on the home page", () => {
        cy.contains("h1", "Welcome to Kraken Bracket");
    });
    it("clicks on profile", () => {
        cy.contains("button", "profile").click();
    });
    it("sees its profile and the edit button", () => {
        const gamerTag = "GamerTag1"
        cy.contains("h1", gamerTag);
        cy.contains("button", "edit");
    });
    it("clicks on edit", () => {
        cy.contains("button", "edit").click();
    });
    //find the name field and change it to something I dono.
    it("clicks on change gamertag", () => {
        const gamerTag = "GamerTag2";
        cy.get(".gamertag-input").type(gamerTag);
        cy.contains("button", "change gamertag").click();
    });
    it("witnesses the new gamertag", () => {
        const gamerTag = "GamerTag2";
        //cy.get(".gamertag-input").type(gamerTag);
    });
    it("clicks on profile again", () => {
        cy.contains("button", "profile").click();
    });
    it("sees its profile and the edit button, the name is updated", () => {
        const gamerTag = "GamerTag2"
        cy.contains("h1", gamerTag);
        cy.contains("button", "edit");
    });
  });