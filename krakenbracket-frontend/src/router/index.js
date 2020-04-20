import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import BracketList from "../views/BracketList.view.vue";
import BracketView from "../views/BracketView.vue";
import BracketRegistrationForm from "@/components/BracketRegistrationForm.vue";
import LoginView from "../views/LoginView.vue"
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
    path: '/login',
    name: 'login-view',
    component: LoginView
  }
];

const router = new VueRouter({
  routes
});

export default router;
