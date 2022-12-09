import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, skipWhile, Subject, switchMap, takeUntil } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Visit } from './visit.model';
import { VisitFormService } from './visit-form.service';
import { VisitsService } from './visits.service';
import { VisitUpsertDialogComponent } from './visit-upsert-dialog/visit-upsert-dialog.component';
import { Patient } from '../patients/patient.model';
import { PatientsService } from '../patients/patients.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-visits',
  templateUrl: './visits.component.html',
  styleUrls: ['./visits.component.scss']
})
export class VisitsComponent {
  public visits$!: Observable<Visit[]>;
  public patients!: Patient[];
  public form!: FormGroup;
  private destroy$ = new Subject<void>();

  constructor(private formService: VisitFormService,
    private dialog: MatDialog,
    private visitsService: VisitsService,
    private patientsService: PatientsService,
    private router: Router){}

  public ngOnInit(): void {
    this.visits$ = this.visitsService.getVisits().pipe(takeUntil(this.destroy$))
    this.patientsService.getPatients().pipe(takeUntil(this.destroy$)).subscribe((patients) =>{
      this.patients = patients;
    })
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public goToPatients(): void {
    this.router.navigate(['/patients'])
  }

  public openDialog(visit?: Visit): void {
    if (visit === undefined) {
      this.formService.initForm();
    } else {
      this.formService.initForm(visit);
    }
    this.form = this.formService.form;

    const dialogRef = this.dialog.open(VisitUpsertDialogComponent, {
      data: {
        patients: this.patients,
        form: this.form,
      },
      autoFocus: false,
    });

    dialogRef
      .afterClosed()
      .pipe(
        takeUntil(this.destroy$),
        skipWhile((result) => result === undefined || !result.isSubmit),
      )
      .subscribe(() => {
        this.upsertVisit(visit?.id);
      });
  }

  private upsertVisit(id?: number): void {
    let data = this.formService.prepareData();

    let upsertedVisit: Visit = {
      id: id,
      date: data.date,
      time: data.time,
      patientId: data.patient?.id!,
    };

    let upsert$: Observable<number>;
    if (id === undefined) {
      upsert$ = this.visitsService.createVisit(upsertedVisit);
    } else {
      upsert$ = this.visitsService.editVisit(upsertedVisit);
    }

    upsert$
      .pipe(
        takeUntil(this.destroy$),
      )
      .subscribe(() =>{
        this.visits$ = this.visitsService.getVisits().pipe(takeUntil(this.destroy$))
      });
  }

  public deleteVisit(id: number): void {
    this.visitsService
      .deleteVisit(id)
      .pipe(
        takeUntil(this.destroy$),
      )
      .subscribe(() => {
        this.visits$ = this.visitsService.getVisits().pipe(takeUntil(this.destroy$))
      });
  }
}
