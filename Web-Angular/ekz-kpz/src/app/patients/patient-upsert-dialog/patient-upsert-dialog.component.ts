import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PatientDialogData } from './patient-upsert-dialog.model';

@Component({
  selector: 'app-patient-upsert-dialog',
  templateUrl: './patient-upsert-dialog.component.html',
  styleUrls: ['./patient-upsert-dialog.component.scss']
})
export class PatientUpsertDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<PatientUpsertDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PatientDialogData,
  ) {}

  public submit(): void {
    if (this.data.form.valid) this.dialogRef.close({ isSubmit: true });
  }

  public cancel(): void {
    this.dialogRef.close({ isSubmit: false });
  }
}
