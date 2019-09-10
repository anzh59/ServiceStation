var React = React;
var Router = ReactRouter;
var Route = ReactRouter.Route;
var Routes = ReactRouter.Routes;
var Navigation = ReactRouter.Navigation;
var History = ReactRouter.History;

import { browserHistory, Router, Route } from 'react-router';

contextTypes: {
    router: React.PropTypes.object.isRequired
}

class LoginForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = { username: "", password: "" };

        this.onSubmit = this.onSubmit.bind(this);
        this.onUserNameChange = this.onUserNameChange.bind(this);
        this.onPasswordChange = this.onPasswordChange.bind(this);
    }
    onUserNameChange(e) {
        this.setState({ username: e.target.value });
    }
    onPasswordChange(e) {
        this.setState({ password: e.target.value });
    }
    onSubmit(e) {
        e.preventDefault();
        var data = new FormData();
        data.append("username", this.state.username.trim());
        data.append("password", this.state.password);
        //this.props.onLoginSubmit({ user: username, password: userpassword });
        this.setState({ username: "", password: "" });

        var xhr = new XMLHttpRequest();
        xhr.open("post", this.props.postUrl, false);
        xhr.onload = function () {
            if (xhr.status == 200) {
                this.context.router.push(this.props.postUrl);
            }
        }.bind(this);
        xhr.send(data);
    }
    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <p>
                    <input type="text"
                        placeholder="Login"
                        value={this.state.username}
                        onChange={this.onUserNameChange} />
                </p>
                <p>
                    <input type="password"
                        value={this.state.password}
                        onChange={this.onPasswordChange} />
                </p>
                <input type="submit" value="Log in" />
            </form>
        );
    }
}
ReactDOM.render(
    <LoginForm postUrl="/Car/List"/>,
    document.getElementById("content")
);