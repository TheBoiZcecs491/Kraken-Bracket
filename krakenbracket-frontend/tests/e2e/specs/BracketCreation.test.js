// https://docs.cypress.io/api/introduction/api.html

describe("Create a bracket", () => {
    it("Visits the app root url", () => {
      cy.visit("http://localhost:8080/#/");
      cy.contains("h1", "Welcome to Kraken Bracket");
    });
    it("Clicks on the 'login' button", () => {
        cy.contains("Log in").click();
      });
    it("Logs the user in", () => {
      const email = "user1@krakenbracket.com";
      const password = "Pass1";
      cy.get(".email-input").type(email);
      cy.get(".password-input").type(password);
      cy.contains("button", "Login").click();
    });
    it("Clicks on the 'Create a new bracket' button", () => {
      cy.contains("button", "Create a new bracket").click();
    });

    it("Makes sure you are on the create bracket page", () => {
      cy.contains("Create a new bracket");
      cy.url().should("include", "/new-bracket");
    });
    it("Fills the bracket form", () => {
        const BracketName = "WNF SFVAE - Pools";
        const PlayerCount = "GamerTag1";
        const GamePlayed = "Street Fighter V - Arcade Edition";
        const GamingPlatform = "Playstation 4";
        const ruleSet = "Single Elimination";
        const startDate = "2020-05-06";
        const startTime = "17:00";
        const endDate = "2020-05-06";
        const endTime = "20:30"
        cy.get(".BracketName-input").type(BracketName);
        cy.get(".PlayerCount-input").type(PlayerCount);
        cy.get(".GamePlayed-input").type(GamePlayed);
        cy.get(".GamingPlatform-input").type(GamingPlatform);
        cy.get(".ruleSet-input").type(ruleSet);
        
        cy.contains("Start Date").click({force:true});
        cy.get('v-datepicker-table').contains(6).click();
        cy.contains("End Date").click({force:true});
        cy.contains("td", "6").click();

        cy.contains("Start Time").click({force:true});
        cy.get(".startTime-input").type(startTime);

        
        cy.contains("End Time").click({force:true});
        cy.get(".endTime-input").type(endTime);
        cy.contains("button", "Create Bracket").click();
      });
      it("Checks if the bracket was successfully created", () => {
          cy.visit("http://localhost:8080/#/bracket-list");
          cy.contains("h1", BracketName);
      })
  });