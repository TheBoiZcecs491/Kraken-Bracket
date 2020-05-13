import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import BracketList from "../views/BracketListView.vue";
import BracketView from "../views/BracketView.vue";
import NewBracketView from "../views/NewBracketView.vue";
import EventList from "../views/EventList.vue";
import EventView from "../views/EventView.vue";
import EventCreate from "../views/EventCreate.vue";
import EventUpdate from "../views/EventUpdate.vue";
import EventRegistrationForm from "@/components/EventRegistrationForm.vue";
import BracketRegistrationForm from "@/components/BracketRegistrationForm.vue";
import LoginView from "../views/LoginView.vue";
import RegisterView from "../views/RegisterView.vue";
import SearchView from "../views/SearchView.vue";
import RegistrationSuccess from "../views/RegistrationSuccess.vue";
import UpdateBracketView from "@/views/UpdateBracketView.vue";
import UserManagementView from "@/views/UserManagementView.vue";

import UserProfileView from "../views/UserProfileView.vue";
Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/about",
    name: "About",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue")
  },
  {
    path: "/bracket-list",
    name: "bracket-list",
    component: BracketList
  },
  {
    path: "/bracket-view/:id",
    name: "bracket-view",
    component: BracketView,
    props: true
  },
  {
    path: "/bracket-view/:id/signup",
    name: "bracket-registration",
    component: BracketRegistrationForm,
    props: true
  },
  {
    path: "/login",
    name: "login-view",
    component: LoginView
  },
  {
    path: "/register",
    name: "register-view",
    component: RegisterView
  },
  {
    path: "/new-bracket",
    name: "new-bracket",
    component: NewBracketView
  },
  {
    path: "/event-list",
    name: "event-list",
    component: EventList
  },
  {
    path: "/event-view/:id",
    name: "event-view",
    component: EventView,
    props: true
  },
  {
    path: "/event-create",
    name: "event-create",
    component: EventCreate
  },
  {
    path: "/event-update",
    name: "event-update",
    component: EventUpdate
  },
  {
    path: "/event-view/:id/signup",
    name: "event-registration",
    component: EventRegistrationForm,
    props: true
  },
  {
    path: "/search",
    name: "search-view",
    component: SearchView,
    props: true
  },
  {
    path: "/registrationSuccess",
    name: "registrationSuccess-view",
    component: RegistrationSuccess,
    props: true
  },
  {
    path: "/:id/bracket-update",
    name: "bracket-update",
    component: UpdateBracketView,
    props: true
  },
  {
    path: "/user-profile",
    name: "user-profile-view",
    component: UserProfileView
  },
  {
    path: "/user-management",
    name: "user-management",
    component: UserManagementView
  }
];

const router = new VueRouter({
  routes,
  mode: "history"
});

export default router;
