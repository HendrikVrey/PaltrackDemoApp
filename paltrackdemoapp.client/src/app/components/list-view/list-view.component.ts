import { Component, OnDestroy, OnInit } from "@angular/core";
import { forkJoin, Observable, Subject, takeUntil } from "rxjs";
import { AppService } from "../../service/app.service";
import type { PersonI } from "../../models/app.models";
import { SavedEvent } from "devextreme/ui/data_grid";
import notify from "devextreme/ui/notify";

@Component({
  selector: "app-list-view",
  standalone: false,
  templateUrl: "./list-view.component.html",
  styleUrls: ["./list-view.component.css"],
})
export class ListViewComponent implements OnInit, OnDestroy {
  //Holdsperson data
  persons: PersonI[] = [];

  //RxJS Subject used for cleanup - emits values when component is destroyed
  private destroy = new Subject<void>();

  constructor(private readonly appService: AppService) { }

  //#region Pagination
  readonly allowedPageSizes = [5, 10];

  readonly displayModes = [{ text: "Display Mode 'full'", value: 'full' }, { text: "Display Mode 'compact'", value: 'compact' }];

  displayMode = 'full';

  showPageSizeSelector = true;

  showInfo = true;

  showNavButtons = true;
  //#endregion

  //#region Lifecycle - Initialized
  ngOnInit() {
    //Start fetching data
    this.appService.getPersons()
      //Chains rxjs operators
      .pipe(takeUntil(this.destroy))  //Auto unsubscribe when destroy produce value
      //Subcribe to receive data updates
      .subscribe(res => {
        //Update component with received data
        this.persons = res;
      });
  }
  //#endregion

  //#region Lifecycle - OnSaved
  onSaved(e: SavedEvent) {
    const operations: Observable<any>[] = [];

    e.changes.forEach(change => {
      try {
        let operation: Observable<any>;
        if (change.type === 'insert') {
          operation = this.appService.createPerson(change.data);
        } else if (change.type === 'update') {
          operation = this.appService.updatePerson(change.key, change.data);
        } else if (change.type === 'remove') {
          operation = this.appService.deletePerson(change.key);
        } else {
          return;
        }
        operations.push(operation);
      } catch (error) {
        this.handleError(error);
      }
    });

    if (operations.length === 0) return;

    //Wait for all operations to complete before refreshing
    forkJoin(operations)
      .pipe(takeUntil(this.destroy))
      .subscribe({
        next: () => this.refreshData(),
        error: (err) => this.handleError(err)
    });
  }
  //#endregion

  //#region Lifecycle - Destroyed
  ngOnDestroy() {
    //Produce value to unsubscribe in takeuntil
    this.destroy.next();
    //Complete to prevent emory leaks
    this.destroy.complete();
  }
  //#endregion

  //#region Helper Methods
  //Refresh data after update, remove or insert
  private refreshData() {
    this.appService.getPersons()
      .pipe(takeUntil(this.destroy))
      .subscribe(res => this.persons = res);
  }

  //Message to user if update,remove or insert failed
  private handleError(error: any) {
    notify("Operation failed", "error", 2000);
  }
  //#endregion

}
