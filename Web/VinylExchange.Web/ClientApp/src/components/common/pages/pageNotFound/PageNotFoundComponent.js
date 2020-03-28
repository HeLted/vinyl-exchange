import React from "react";
import ClientError from "./../../errors/ClientError";

function PageNotFoundComponent() {
  return (
    <ClientError
      statusCode={404}
      heading={"Page Not Found!"}
      info={"The page or resource you are looking for is nowhere to be found!"}
    />
  );
}

export default PageNotFoundComponent;
