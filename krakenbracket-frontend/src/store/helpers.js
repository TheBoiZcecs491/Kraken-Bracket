import { mapGetters } from "vuex";

export const authComputed = {
  ...mapGetters(["loggedIn"]),
  ...mapGetters(["bracketPlayerInfo"]),
  ...mapGetters(["userInformation"]),
  ...mapGetters(["gamerInfo"])
};
