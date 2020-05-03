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
      cy.get('input[type="email"]').type("docjohnson9001@fancypants.com");
      cy.get('input[type="password"]').type("wpAcWM&235xd");
      cy.get('input[type="firstName"]').type("Johnson");
      cy.get('input[type="lastName"]').type("Daneeka");
      cy.contains("Register User").click();
    });
    it("If it worked we should be back on the homepage without the login button", () => {
      cy.contains("h1", "Welcome to Kraken Bracket");
      cy.contains("login").should('not.exist');
    });
  });

  describe("Create a new account, but the email already exists", () => {
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
      cy.get('input[type="email"]').type("docjohnson9001@fancypants.com");
      cy.get('input[type="password"]').type("wpAcWM&235xd");
      cy.get('input[type="firstName"]').type("Johnson");
      cy.get('input[type="lastName"]').type("Daneeka");
      cy.contains("Register User").click();
    });
    it("error 406 should be visable", () => {
      cy.contains("h1", "Register new user");
      cy.contains("406");
    });
  });

  describe("Create a new account, but the password is insecure", () => {
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
      cy.get('input[type="email"]').type("docjohnson9002@fancypants.com");
      cy.get('input[type="password"]').type("123love");
      cy.get('input[type="firstName"]').type("Johnson");
      cy.get('input[type="lastName"]').type("Daneeka");
      cy.contains("Register User").click();
    });
    it("error 406 should be visable", () => {
      cy.contains("h1", "Register new user");
      cy.contains("406");
    });
  });

  describe("Create a new account, but they didnt put the names in", () => {
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
      cy.get('input[type="email"]').type("docjohnson9002@fancypants.com");
      cy.get('input[type="password"]').type("wpAcWM&235xd");
      //cy.get('input[type="firstName"]').type("");
      //cy.get('input[type="lastName"]').type("");
      cy.contains("Register User").click();
    });
    it("error 406 should be visable", () => {
      cy.contains("h1", "Register new user");
      cy.contains("406");
    });
  });

  describe("Create a new account, but that isnt even a valid email", () => {
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
      cy.get('input[type="email"]').type("ME");
      cy.get('input[type="password"]').type("wpAcWM&235xd");
      cy.get('input[type="firstName"]').type("Johnson");
      cy.get('input[type="lastName"]').type("Daneeka");
      cy.contains("Register User").click();
    });
    it("error 406 should be visable", () => {
      cy.contains("h1", "Register new user");
      cy.contains("406");
    });
  });

  describe("Create a new account, again after the first one", () => {
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
      cy.get('input[type="email"]').type("fredcougar123@fuzzballs.net");
      cy.get('input[type="password"]').type("wpAcWM&235xd");
      cy.get('input[type="firstName"]').type("Johnson");
      cy.get('input[type="lastName"]').type("Daneeka");
      cy.contains("Register User").click();
    });
    it("If it worked we should be back on the homepage without the login button", () => {
      cy.contains("h1", "Welcome to Kraken Bracket");
      cy.contains("login").should('not.exist');
    });
  });