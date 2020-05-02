import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import BracketList from "../views/BracketListView.vue";
import BracketView from "../views/BracketView.vue";
import NewBracket from "../views/NewBracket.vue";
import EventList from "../views/EventList.vue";
import EventView from "../views/EventView.vue";
import SearchView from "../views/SearchView.vue";
import BracketRegistrationForm from "@/components/BracketRegistrationForm.vue";
import LoginView from "../views/LoginView.vue";
import SearchView from "../views/SearchView.vue";
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
    path: "/new-bracket",
    name: "new-bracket",
    component: NewBracket
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
    path: "/search",
    name: "search-view",
    component: SearchView,
    props: true
  }
];

const router = new VueRouter({
  routes,
  mode: "history"
});

export default router;
