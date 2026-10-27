# Lecturer restoration record

This project was reconstructed from the user-provided `KKV2-master(1).zip` and code exchanged in the conversation.

**It is not the original lost Git repository.** The initial commit is an archive snapshot, and the later commits are a reconstruction made now. No historical timestamps or SHAs were forged.

The old public repository `melon3000/KelteKool-V2-Testing` retains its own genuine commits through 2026-10-15. To retain that history, clone the old repository and replay the `steps/` with `apply_steps.py` on a fresh branch. Its baseline is not identical: it has plural `LecturersServices` names and may need a bridge commit aligning the October ZIP snapshot.

The final snapshot implements Lecturer CRUD, 9 backend xUnit tests and 4 Selenium tests. The same results were reported by the user for the previous, now lost environment, but they have NOT been rerun in this reconstruction because the environment lacks .NET SDK and Firefox.

For Selenium, launch the web app separately on `http://localhost:5117` before running browser tests.

Potential improvements (not represented as proven original changes): test isolation: multiple `DetailsTest` / `UpdateTest` rows remain possible after repeated runs; repeated HTML IDs exist in the index view. Review any database differences before replay.
