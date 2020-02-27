import { Headers } from "./../constants/UrlConstants";
import Cookies from "js-cookie";

export default function functiongetAntiForgeryAxiosConfig() {
  return {
    headers: {
      [Headers.requestVerificationToken]: Cookies.get("XSRF-TOKEN")
    }
  };
}
