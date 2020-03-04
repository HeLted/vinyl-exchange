import React from "react";
import PlayerLoaderButton from "./../../../common/PlayerLoaderButton";
import AddtoColletionPopupFormContainer from "./../../../common/popupForms/addToCollectionPopupForm/AddToCollectionPopupFormContainer";
import AddSalePopupContainer from "./../../../common/popupForms/addSalePopupForm/AddSalePopupContainer";

function ReleaseMenuComponent(props) {
  return (
    <div className="btn-group" role="group">
      <PlayerLoaderButton data={{ releaseId: props.data.releaseId }} />
      <AddSalePopupContainer key={Math.random()}
        data={{
          releaseId: props.data.releaseId
        }}
      />
      <AddtoColletionPopupFormContainer key={Math.random()}
        data={{ releaseId: props.data.releaseId }}
      />
    </div>
  );
}

export default ReleaseMenuComponent;
