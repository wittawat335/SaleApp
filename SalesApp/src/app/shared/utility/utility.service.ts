import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Session } from '../interfaces/session';

@Injectable({
  providedIn: 'root',
})
export class UtilityService {
  constructor(private snackBar: MatSnackBar) {}

  showAlert(message: string, action: string) {
    this.snackBar.open(message, action, {
      horizontalPosition: 'end',
      verticalPosition: 'top',
      duration: 3000,
    });
  }

  setSessionUser(userSession: Session) {
    localStorage.setItem('user', JSON.stringify(userSession));
  }

  getSessionUser() {
    const data = localStorage.getItem('user');
    const user = JSON.parse(data!);

    return user;
  }

  removeSessionUser() {
    localStorage.removeItem('user');
  }
}
