import { Component, ViewChild, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  constructor(private router: Router) { }

  submitButtonOptions = {
    type: "default",
    text: "Login",
    icon: "login",
    useSubmitBehavior: true
  }

  handleSubmit = (e: { preventDefault: () => void }): void => {
    e.preventDefault();
    this.login();
  }

  private login(): void {
    this.router.navigate(['/list']);
  }
}
