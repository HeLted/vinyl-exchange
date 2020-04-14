import React, { Fragment } from "react";
import PlayerLoaderButton from "./../../../common/PlayerLoaderButton";
import AddtoColletionPopupFormContainer from "./../../../common/popupForms/addToCollectionPopupForm/AddToCollectionPopupFormContainer";
import AddSalePopupContainer from "./../../../common/popupForms/addSalePopupForm/AddSalePopupContainer";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";

function ReleaseMenuComponent(props) {
  return props.data.isLoading ? (
    <BorderSpinner />
  ) : (
    <div className="row text-center justify-content-center">
      <PlayerLoaderButton data={{ releaseId: props.data.releaseId }} />

      {props.data.user !== null && (
        <Fragment>
          {" "}
          <AddSalePopupContainer
            disabled={props.data.user !== null}
            data={{
              releaseId: props.data.releaseId
            }}
          />
          <AddtoColletionPopupFormContainer
            data={{ releaseId: props.data.releaseId }}
          />
        </Fragment>
      )}
    </div>
  );
}

export default ReleaseMenuComponent;
