<template>
  <v-app>
    <div>
      <h1>Signup</h1>
      <v-form>
        <v-container fluid>
          <v-layout row>
            <v-flex xs4>
              <!-- This element's content is intentionally empty -->
            </v-flex>
            <v-flex xs4>
              <v-text-field
                label="Email"
                type="email"
                placeholder="john@gmail.com"
                :rules="emailRules"
                required
              ></v-text-field>
              <v-text-field label="GamerTag" type="text"></v-text-field>
              <v-btn type="submit" color="primary">Register!</v-btn>
            </v-flex>
            <v-flex xs4>
              <!-- This element's content is intentionally empty -->
            </v-flex>
          </v-layout>
        </v-container>
      </v-form>
    </div>
  </v-app>
</template>

<script>
export default {
  data: () => ({
    emailRules: [
      email => !!email || "Email is required",
      email => email.indexOf("@") !== 0 || "Email should have a name before it",
      email => email.includes("@") || "Email should include @ symbol",
      email =>
        email.indexOf(".") - email.indexOf("@") > 1 ||
        "Email should contain a valid domain name",
      email =>
        (email.length > 5 && email.length <= 200) || "Invalid email length"
    ]
  })
};
</script>
