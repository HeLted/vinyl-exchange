import React from "react";
import ClientError from "./../../errors/ClientError";

function ServerErrorComponent() {
  return (
    <ClientError
      statusCode={500}
      heading={"Internal Server Error"}
      info={"There was an unexpected error on the server!"}
    />
  );
}

export default ServerErrorComponent;
