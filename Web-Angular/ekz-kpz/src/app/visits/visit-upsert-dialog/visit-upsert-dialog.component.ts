import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { VisitDialogData } from './visit-upsert-dialog.model';

@Component({
  selector: 'app-visit-upsert-dialog',
  templateUrl: './visit-upsert-dialog.component.html',
  styleUrls: ['./visit-upsert-dialog.component.scss']
})
export class VisitUpsertDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<VisitUpsertDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: VisitDialogData,
  ) {
    console.log(data)
  }

  public objectComparison(option: any, value: any): boolean {
    return option?.id === value?.id;
  }

  public submit(): void {
    if (this.data.form.valid) this.dialogRef.close({ isSubmit: true });
  }

  public cancel(): void {
    this.dialogRef.close({ isSubmit: false });
  }
}
