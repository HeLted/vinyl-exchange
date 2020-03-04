import React, { Fragment, Component } from "react";
import "./ReleasesTable.css";
import InfiniteScroll from "react-infinite-scroller";
import { css } from "@emotion/core";
import PulseLoader from "react-spinners/PulseLoader";
import PlayerLoaderButton from "./../../common/PlayerLoaderButton";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMeh } from "@fortawesome/free-solid-svg-icons";
import { Url } from "./../../../constants/UrlConstants";
import {Link} from "react-router-dom";

class ReleasesComponent extends Component {
  constructor() {
    super();
    this.state = { loadEjectButtons: [] };
  }

  render() {
    const releases = this.props.data.releases.map(release => {
      const coverArtimageSource =
        release.coverArt !== null
          ? Url.mediaStorage + release.coverArt.path + release.coverArt.fileName
          : "https://cdn4.iconfinder.com/data/icons/ui-beast-4/32/Ui-12-512.png";

      return (
        <Fragment key={release.id}>
          <tr
            onClick={() =>
              this.props.functions.handleRedirectToRelease(release.id)
            }
          >
            <td>
              <img src={coverArtimageSource} width="80" height="80" alt="" />
            </td>
            <td>
              <PlayerLoaderButton data={{ releaseId: release.id }} />
            </td>
            <td>{release.artist}</td>
            <td>{release.title}</td>
            <td>
              {release.label} - {release.year} - {release.format}
            </td>
          </tr>
        </Fragment>
      );
    });

    const loader = (
      <tr key="release loader">
        <td>
          <PulseLoader
            size={15}
            //size={"150px"} this also works
            color={"#13eddb"}
            loading={this.props.data.isLoadMoreReleasesLoading}
          />
        </td>
      </tr>
    );

    return (
      <table
        className="releases-table table-hover"
        ref={ref => (this.scrollParentRef = ref)}
      >
        <InfiniteScroll
          initialLoad={false}
          hasMore={this.props.data.isThereMoreReleasesToLoad}
          loadMore={() => this.props.functions.handleLoadReleases(false)}
          loader={loader}
          element={"tbody"}
          useWindow={false}
          getScrollParent={() => this.scrollParentRef}
        >
          {releases}
        </InfiniteScroll>

        {(this.props.data.isThereMoreReleasesToLoad === false &&
          releases.length > 6) ||
        (releases.length === 0 &&
          this.props.data.isLoadMoreReleasesLoading === false) ? (
          <tbody>
            <tr>
              <td colSpan="3">
                <div className="search-not-found">
                  <h6>
                    <b>
                      <i>
                        <FontAwesomeIcon icon={faMeh} /> No More Releases.Maybe
                        Try Another Search Term or{"    "}
                        <Link
                          className="btn btn-outline-secondary"
                          to="/Releases/AddRelease"
                        >
                           Add Release
                        </Link>
                      </i>
                    </b>
                  </h6>
                </div>
              </td>
            </tr>
          </tbody>
        ) : null}
      </table>
    );
  }
}

export default ReleasesComponent;
