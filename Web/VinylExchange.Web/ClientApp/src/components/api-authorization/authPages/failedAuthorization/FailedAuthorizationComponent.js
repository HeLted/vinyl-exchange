import React, { Component } from "react";
import ClientError from "./../../../common/errors/ClientError"

class FailedAuthorizationComponent extends Component {
  render() {
    return (
      <ClientError
        statusCode={403}
        heading={"Resource Forbidden"}
        info={`Sorry, your access is refused due to security reasons of our
      server and also our sensitive data.
      
      Please go back to the previous page to continue browsing.`}
      />
    );
  }
}

export default FailedAuthorizationComponent;
