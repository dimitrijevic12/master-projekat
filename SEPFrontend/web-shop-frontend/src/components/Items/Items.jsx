import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { getItems, getImagesForItems } from "../../actions/actionsItems";

class Items extends Component {
  state = {
    showPostModal: false,
    postId: 0,
    username: "",
  };
  async componentDidMount() {
    debugger;
    await this.props.getItems();
    await this.getAllImages(this.props.items);
  }
  render() {
    if (this.props.items === undefined) {
      return null;
    }

    debugger;
    const items = this.props.items;

    if (this.props.itemsImages === undefined) {
      return null;
    }
    debugger;
    const ItemsList = () => {
      debugger;
      return items.map((post, i) =>
        this.props.itemsImages[i].contentType === "image/jpeg" ? (
          <Photo
            src={
              "data:image/jpg;base64," + this.props.itemsImages[i].fileContents
            }
            onClick={() => this.view(post)}
          />
        ) : (
          <video
            controls
            onClick={() => this.view(post)}
            style={{ width: 367, height: 370 }}
            className="mb-3"
          >
            <source
              src={
                "data:video/mp4;base64," +
                this.props.itemsImages[i].fileContents
              }
              type="video/mp4"
            ></source>
          </video>
        )
      );
    };

    return (
      <div>
        {/* {this.state.showPostModal ? (
          <PostModal
            show={this.state.showPostModal}
            postId={this.state.postId}
            personPhoto="/images/download.jfif"
            person={this.state.username}
            onShowChange={() => this.displayModalPost()}
          />
        ) : null} */}
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>Items</GridControlBarItem>
          </GridControlBar>
          <ItemsList />
        </Grid>
      </div>
    );
  }

  view(f) {
    localStorage.setItem("item-productkey", f.productKey);
    window.location = "/item/" + f.productKey;
  }

  //   displayModalPost = (post) => {
  //     if (post != undefined) {
  //       this.setState({
  //         postId: post.id,
  //         username: post.registeredUser.username,
  //       });
  //     }
  //     this.setState({
  //       showPostModal: !this.state.showPostModal,
  //     });
  //   };

  getAllImages = async (items) => {
    debugger;
    const paths = [];
    for (var i = 0; i < items.length; i++) {
      if (items[i].imagePath === undefined) {
        paths.push(items[i].imagePaths[0]);
      } else {
        paths.push(items[i].imagePath);
      }
    }
    await this.props.getImagesForItems(paths);
  };
}

const mapStateToProps = (state) => ({
  items: state.items,
  itemsImages: state.itemsImages,
});

export default compose(
  connect(mapStateToProps, {
    getItems,
    getImagesForItems,
  })
)(Items);
