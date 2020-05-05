// https://docs.cypress.io/api/introduction/api.html

describe("Tests the start and end date range", () => {
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
  it("Checks an end date before the start date", () => {
    cy.get("#input-61").click();
    cy.get('.menuable__content__active > .v-picker > .v-picker__body > :nth-child(1) > .v-date-picker-table > table > tbody > :nth-child(2) > :nth-child(3) > .v-btn').click();
        
    cy.get("#input-53").click();
    cy.get('.menuable__content__active > .v-picker > .v-picker__body > :nth-child(1) > .v-date-picker-table > table > tbody > :nth-child(2) > :nth-child(4) > .v-btn').click();
        
  });
});


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
        cy.get('#list-item-70-0 > .v-list-item__content > .v-list-item__title').click();
        cy.get(".GamingPlatform-input").type(GamingPlatform);
        cy.get('#list-item-90-0').click();
        cy.get(".ruleSet-input").type(ruleSet);
        // start date
        cy.get("#input-53").click();
        cy.get(':nth-child(2) > :nth-child(4) > .v-btn > .v-btn__content').click();
        // start time
        cy.pause();
        //cy.get("#input-57").click();
        //cy.get('[style="left: 75%; top: 93.3013%;"]').click();
        //cy.contains("30").click();
        //cy.get('[style="left: 75%; top: 93.3013%;"] > span').click({multiple:true});
        //cy.get('.v-time-picker-title__ampm > :nth-child(2)').click();
        // end date
        cy.get("#input-61").click();
        cy.get('.menuable__content__active > .v-picker > .v-picker__body > :nth-child(1) > .v-date-picker-table > table > tbody > :nth-child(2) > :nth-child(4) > .v-btn').click();
        //.click()).then(cy.contains("v-time-picker-clock__item", 00)
        //.click()).then(cy.contains("v-time-picker-clock__item", "PM")
        //.click());
        //cy.contains("End Date").click({force:true}).then(cy.get('tr').contains("6").click());
        //cy.contains("End Time").click({force:true}).then(cy.contains("tr", "8").click()).then(cy.contains("tr", "30").click()).then(cy.contains("tr", "PM").click());
        //cy.contains("").click();
        // cy.get('tr').contains("6").click()
        // cy.contains("End Date").click({force:true});
        // cy.contains("td", "6").click();

        // cy.get(".startTime-input").type(startTime);

        
        // cy.contains("End Time").click({force:true});
        // cy.get(".endTime-input").type(endTime);
        cy.contains("button", "Create Bracket").click();
      });
      it("Checks if the bracket was successfully created", () => {
          const BracketName = "WNF SFVAE - Pools";
          cy.contains("Bracket List").click();
          cy.contains(BracketName);
      })
  });

  describe("Tests the ruleset text field for a 700 char limit", () => {
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
    it("Fills in the text field", () => {
      const ruleSet = "This is a very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very very very very very very very " +
       "very very very very very long description of the rule set";
      cy.get(".ruleSet-input").type(ruleSet);
    });
  });

  