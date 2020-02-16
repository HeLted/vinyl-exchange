import "bootstrap/dist/css/bootstrap.css";
import "./../node_modules/dropzone/dist/dropzone.css"
import "./../node_modules/bootstrap/dist/js/bootstrap.bundle.min"
import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import registerServiceWorker from "./registerServiceWorker";
import Dropzone from "dropzone"

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <App />
  </BrowserRouter>,
  rootElement);

registerServiceWorker();

